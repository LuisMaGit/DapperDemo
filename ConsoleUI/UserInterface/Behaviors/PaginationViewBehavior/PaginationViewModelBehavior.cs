using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleUI.UserInterface.Helpers;
using DataManager.DataAccess.PersonServices;
using DataManager.Domain;

namespace ConsoleUI.UserInterface.Behaviors.PaginationViewBehavior
{
    public class PaginationViewModelBehavior
    {
        private readonly IPersonService _personService;
        private readonly PaginationView _view;
        private readonly ResponseBehaviors _responseBehaviors;
        private readonly SimpleValidationBehavior _simpleValidationBehavior;

        public int CurrentPage { get; private set; } = 1;
        private int _totalPages = 0;

        public PaginationViewModelBehavior(
            IPersonService personService,
            ResponseBehaviors responseBehaviors,
            PaginationView view,
            SimpleValidationBehavior simpleValidationBehavior)
        {
            _personService = personService;
            _responseBehaviors = responseBehaviors;
            _view = view;
            _simpleValidationBehavior = simpleValidationBehavior;
        }

        private void _InsertPageSing()
        {
            _view.InsertPageNumber(1, _totalPages);
        }

        private bool _ValidateInput(string input)
        {
            return Validators.ValidatePageRange(input, _totalPages);
        }

        public async Task RunPaginationWith<T>(Func<Task<DataResponseModel<List<T>>>> creationCall)
        {
            do
            {
                var result = await creationCall();
                var responseWithData = _responseBehaviors.HandleListDataResponse(result);
                if (!responseWithData) break;

                var total = result.Pagination.Total;
                var responseCount = result.Data.Count;
                var reachLimit = total == responseCount;

                //Not need for pagination
                if (reachLimit)
                {
                    _view.Total(total);
                    break;
                }

                //Ask for page
                _totalPages = result.Pagination.Pages;
                _view.Total(total);
                _view.Pagination(CurrentPage, _totalPages);

                //Validate page
                var input = _simpleValidationBehavior.ValidationLogic(_InsertPageSing, _ValidateInput);
                //Check Exit
                if (input == Constants.EXIT_CHAR) break;

                //Current page to ask
                CurrentPage = int.Parse(input);
            } while (true);
        }
    }
}