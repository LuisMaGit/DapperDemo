using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.UserInterface.Behaviors;
using ConsoleUI.UserInterface.Helpers;
using DataManager.DataAccess.PersonServices;

namespace ConsoleUI.UserInterface.Views.InsertPerson
{
    public class InsertPersonViewModel
    {
        private readonly IPersonService _personService;
        private readonly InsertPerson _view;
        private readonly SimpleValidationBehavior _validationBehavior;
        private readonly ResponseBehaviors _responseBehaviors;
        private Dictionary<InsertPersonActions, InsertPersonActionsModels> _insertPersonActionsModel;

        private List<string> _addresses;

        public InsertPersonViewModel(
            IPersonService personService,
            InsertPerson view,
            SimpleValidationBehavior validationBehavior,
            ResponseBehaviors responseBehaviors)
        {
            _personService = personService;
            _view = view;
            _validationBehavior = validationBehavior;
            _responseBehaviors = responseBehaviors;
            _MapActions();
        }

        private void _MapActions()
        {
            var name = new InsertPersonActionsModels
            {
                InsertSign = _view.EnterName,
                Validation = Validators.ValidateName
            };
            var lastName = new InsertPersonActionsModels
            {
                InsertSign = _view.EnterLastName,
                Validation = Validators.ValidateLastName
            };
            var email = new InsertPersonActionsModels
            {
                InsertSign = _view.EnterEmail,
                Validation = Validators.ValidateEmail
            };
            var phone = new InsertPersonActionsModels
            {
                InsertSign = _view.EnterPhone,
                Validation = Validators.ValidatePhone
            };
            var addresses = new InsertPersonActionsModels
            {
                InsertSign = _view.EnterAdress,
                Validation = Validators.ValidateAddress
            };

            _insertPersonActionsModel = new Dictionary<InsertPersonActions, InsertPersonActionsModels>
            {
                {InsertPersonActions.Name, name},
                {InsertPersonActions.LastName, lastName},
                {InsertPersonActions.Phone, phone},
                {InsertPersonActions.Email, email},
                {InsertPersonActions.Addresses, addresses},
            };
        }

        public async Task InitViewModel()
        {
            foreach (var (key, value) in _insertPersonActionsModel)
            {
                if (key == InsertPersonActions.Addresses)
                {
                    break;
                }

                value.Value =
                    _validationBehavior.ValidationLogic(value.InsertSign, value.Validation);
            }

            _FormAddresses();
            await _InsertPerson();
        }

        private void _FormAddresses()
        {
            var keepLooking = true;
            _addresses = new List<string>();
            _view.EnterAdresses();
            var insertAction = _insertPersonActionsModel[InsertPersonActions.Addresses];
            do
            {
                var input = _validationBehavior.ValidationLogic(insertAction.InsertSign, insertAction.Validation);
                if (input != Constants.EXIT_CHAR)
                {
                    _addresses.Add(input);
                }
                else
                {
                    keepLooking = false;
                }
            } while (keepLooking);
        }

        private async Task _InsertPerson()
        {
            var name = _insertPersonActionsModel[InsertPersonActions.Name].Value;
            var lastName = _insertPersonActionsModel[InsertPersonActions.LastName].Value;
            var email = _insertPersonActionsModel[InsertPersonActions.Email].Value;
            var phone = _insertPersonActionsModel[InsertPersonActions.Phone].Value;

            var response = await _personService.SavePersonAsTransactionAsync(name, lastName, email, phone, _addresses);
            _responseBehaviors.HandleSingleDataResponse(response);
        }
    }
}