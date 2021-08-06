using System.Collections.Generic;
using System.Linq;
using ConsoleUI.UserInterface.Services;
using DataManager.Domain;

namespace ConsoleUI.UserInterface.Behaviors
{
    public class ResponseBehaviors
    {
        private readonly ApplicationUiService _appUi;

        public ResponseBehaviors(ApplicationUiService appUi)
        {
            _appUi = appUi;
        }

        private static string _ErrorStr(string[] errors)
        {
            var output = "";
            for (var i = 0; i < errors.Length; i++)
            {
                output += errors[i] + (i != 0 ? "\n" : "");
            }

            return output;
        }

        private bool _HandleResponseErros(string[] errors)
        {
            if (!errors.Any()) return false;
            _appUi.ErrorResponse(_ErrorStr(errors));
            return true;
        }

        public void HandleSingleDataResponse<T>(DataResponseModel<T> response)
        {
            if (_HandleResponseErros(response.Errors))
            {
                return;
            }

            if (response.Data == null)
            {
                _appUi.EmptyResponse();
                return;
            }

            _appUi.OkResponse(response.Data);
        }

        public void HandleListDataResponse<T>(DataResponseModel<List<T>> response)
        {
            if (_HandleResponseErros(response.Errors))
            {
                return;
            }

            if (response.Data == null || !response.Data.Any())
            {
                _appUi.EmptyResponse();
                return;
            }

            _appUi.OkListResponse(response.Data);
        }
    }
}