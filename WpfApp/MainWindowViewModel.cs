using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace WpfApp
{
    /// <summary>
    /// ViewModel of main window.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Coordinates _Coords = new Coordinates();
        private string _InputFilePath;
        private string _OutputFilePath = "formatted data.txt";
        private string _UserInput;
        private string _ResultDemo;
        /// <summary>
        /// Path to a file from which data should be read.
        /// </summary>
        public string InputFilePath
        {
            get
            {
                return _InputFilePath;
            }
            set
            {
                _InputFilePath = value;
                OnPropertyChanged("InputFilePath");
            }
        }
        /// <summary>
        /// Path to a file where data should be stored.
        /// </summary>
        public string OutputFilePath
        {
            get
            {
                return _OutputFilePath;
            }
            set
            {
                _OutputFilePath = value;
                OnPropertyChanged("OutputFilePath");
            }
        }
        /// <summary>
        /// Data that user entered from the window
        /// </summary>
        public string UserInput
        {
            get
            {
                return _UserInput;
            }
            set
            {
                _UserInput = value;
                OnPropertyChanged("CoordsInOneString");
            }
        }
        /// <summary>
        /// Data in format for demonstraton to user in window.
        /// </summary>
        public string ResultDemo
        {
            get
            {
                return _ResultDemo;
            }
            set
            {
                _ResultDemo = value;
                OnPropertyChanged("ResultDemo");

            }
        }

        /// <summary>
        /// Constructor of ViewModel.
        /// </summary>
        public MainWindowViewModel()
        {
            #region commands
            HandleDataWithFilesCommand =
                new Command(OnHandleDataWithFilesCommandExecuted, CanHandleDataWithFilesCommandExecute);
            HandleDataFromUserInputCommand =
                new Command(OnHandleDataFromUserInputCommandExecuted, CanHandleDataFromUserInputCommandExecute);
            #endregion
        }

        #region commands
        /// <summary>
        /// Command for handling data with reading and writing it to a files.
        /// </summary>
        public ICommand HandleDataWithFilesCommand { get; }
        /// <summary>
        /// Action of HandleDataWithFilesCommand
        /// </summary>
        /// <param name="parameter"></param>
        public void OnHandleDataWithFilesCommandExecuted(object parameter)
        {
            try
            {
                _Coords.CoordsData = _Coords.GetStringsFromFile(InputFilePath);
                _Coords.CoordsData = _Coords.FormatData(_Coords.CoordsData);
                _Coords.WriteToFile(_Coords.CoordsData, OutputFilePath);
                ResultDemo = String.Join("\n", _Coords.CoordsData) + $"\n\ndata was written to \"{OutputFilePath}\"";
            }
            catch (Exception e)
            {
                ResultDemo = "\nError! " + e.Message;
            }
        }
        /// <summary>
        /// Condition under which HandleDataWithFilesCommand can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanHandleDataWithFilesCommandExecute(object parameter)
        {
            if (InputFilePath != null && OutputFilePath != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        ///  Command for handling data located in IserInput property and writing it to a file
        /// </summary>
        public ICommand HandleDataFromUserInputCommand { get; }
        /// <summary>
        /// Action of HandleDataFromUserInputCommand.
        /// </summary>
        /// <param name="parameter"></param>
        public void OnHandleDataFromUserInputCommandExecuted(object parameter)
        {
            try
            {
                _Coords.CoordsData = UserInput.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                _Coords.CoordsData = _Coords.FormatData(_Coords.CoordsData);
                _Coords.WriteToFile(_Coords.CoordsData, OutputFilePath);
                ResultDemo = String.Join("\n", _Coords.CoordsData) + $"\n\ndata was written to \"{OutputFilePath}\"";
            }
            catch (Exception e)
            {
                ResultDemo = "\nError! " + e.Message;
            }

        }
        /// <summary>
        /// Condition under which HandleDataFromUserInputCommand can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanHandleDataFromUserInputCommandExecute(object parameter)
        {
            if (UserInput != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Event to notify View when some property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Must be invoked in setters of every property bound to View to notify it about changes.
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
