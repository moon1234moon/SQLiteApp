using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ViewModels
{
    public class LoginViewModel : Screen
    {
        #region
        private string _username = "admin";
        private string _password = "admin";
        private string _error_message;
        private IEventAggregator _events;
        #endregion

        public LoginViewModel(IEventAggregator events)
        {
            _events = events;
        }

        #region
        public string ErrorMessage
        {
            get { return _error_message; }
            set
            {
                _error_message = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if(ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (Username?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }
        #endregion

        public void LogIn()
        {
            //try
            //{
            //    var result = await _api_helper.Authenticate(Username, Password);
            //    ErrorMessage = string.Empty;

            //    // Capture more information about the user
            //    await _api_helper.GetLoggedInUserInfo(result.Access_Token);

            //    _events.PublishOnUIThread(new LogOnEvent());
            //}
            //catch (Exception ex)
            //{
            //    ErrorMessage = ex.Message;
            //}
        }
    }
}
