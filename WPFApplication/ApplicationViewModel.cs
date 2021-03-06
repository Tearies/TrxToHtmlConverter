﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFApplication
{
    class ApplicationViewModel : ViewModelBase
    {
        static ApplicationModel app = new ApplicationModel();
        public ICommand SearchFileCommand { private set; get; }
        public ICommand SearchExeCommand { private set; get; }
        public ICommand ConvertCommand { private set; get; }
        public ICommand OpenCommand { private set; get; }

        public string FilePath
        {
            set => SetProperty(ref app.filePath,value );
            get => app.filePath;
        }

        public bool IsTestCategorySearchChecked
        {
            set
            {
                if (app.isTestCategorySearchChecked == value) return;

                app.isTestCategorySearchChecked = value;
                TestCategoryEnable = app.isTestCategorySearchChecked ? true : false;
            }
            get => app.isTestCategorySearchChecked;
        }

        public string TestCategory
        {
            set => SetProperty(ref app.testCategory, value);
            get => app.testCategory;
        }

        public string VsTestConsolePath
        {
            set => SetProperty(ref app.vsTestConsolePath, value);
            get => app.vsTestConsolePath;
        }

        public string ChangeSetNumber
        {
            set => SetProperty(ref app.changeSetNumber, value);
            get => app.changeSetNumber;
        }

        public string PbiNumber
        {
            set => SetProperty(ref app.pbiNumber, value);
            get => app.pbiNumber;
        }

        public string OutputPath
        {
            set => SetProperty(ref app.outputPath, value);
            get => app.outputPath;
        }

        public string Result
        {
            private set => SetProperty(ref app.result, value);
            get => app.result;
        }

        public bool EnableToOpen
        {
            private set => SetProperty(ref app.enableToOpen, value);
            get => app.enableToOpen;
        }

        public bool TestCategoryEnable
        {
            private set => SetProperty(ref app.testCategoryEnable, value);
            get => app.testCategoryEnable;
        }

        void OpenHtmlFile()
        {
            System.Diagnostics.Process.Start(OutputPath);
        }

        public ApplicationViewModel()
        {
            SearchFileCommand = new RelayCommand((obj) => { FilePath = app.OpenFileDialog("dll"); });
            SearchExeCommand = new RelayCommand((obj) => { VsTestConsolePath = app.OpenFileDialog("exe"); });
            ConvertCommand = new RelayCommand((obj) =>
            {
                if (FilePath != null)
                {
                    OutputPath = FilePath.Replace(".dll", ".html");
                    app.Convert();
                    //OutputPath = FilePath.Replace(".trx", ".html");
                    Result = "Exported file to:\n" + OutputPath;
                    EnableToOpen = true;
                }
            });
            OpenCommand = new RelayCommand((obj) => { OpenHtmlFile(); });
        }
    }
}
