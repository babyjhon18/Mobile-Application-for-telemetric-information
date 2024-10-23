using INDELAPPEnd.Models;
using INDELLAPPEnd.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INDELAPPEnd.Pages
{
    /*
         * Объект:
         * 0 - Выбор мест
         * 1 - Выбор типа объекта
         * 2 - Выбор потребителя
         * 3 - Выбор типа соединения
         * Прибор учета:
         * 4 - Выбор типа ПУ
         * 5 - Выбор типа учета
         * 6 - Выбор среды учета
         * 7 - Выбор состояния
         * 8 - Выбор скорости
         * 9 - Выбор типа контроллера
         * 10 - типа архивных данных 
    */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        private CustomLocationClass SelectedLocation = new CustomLocationClass();
        private BaseItemClass SelectedItemFromAnyField = new BaseItemClass();
        private ObjectTypeClass SelectedObjectType = new ObjectTypeClass();
        public List<BaseItemClass> ArchiveTypeList = new List<BaseItemClass>()
        {
            new BaseItemClass() { ID = 0, Name = "Текущие" },
            new BaseItemClass() { ID = 1, Name = "Суточные" },
            new BaseItemClass() { ID = 2, Name = "Часовые" },
            new BaseItemClass() { ID = 3, Name = "Минутные" },
        };
        private string SelectedSpeed { get; set; }
        private ParentedContactClass SelectedConsumer = new ParentedContactClass();
        public int ListType { get; set; }
        public ListViewPage()
        {
            InitializeComponent();
        }

        public ListViewPage(ObservableCollection<CustomRegionClass> regions, int index, string LocationName)
        {
            InitializeComponent();
            ListType = index;
            anyField.IsVisible = false;
            anyFieldFrame.IsVisible = false;
            foreach (var region in regions)
            {
                RegionLocation.Children.Add(new Label()
                {
                    Text = region.name,
                    TextColor = Color.Black,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14
                });
                foreach (var location in region.Locations)
                {
                    RadioButton locationRadioButton = new RadioButton()
                    {
                        Content = location.name,
                        Value = location.name,
                        BindingContext = location,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (location.name == LocationName)
                    {
                        locationRadioButton.IsChecked = true;
                        SelectedLocation = location;
                    }
                    locationRadioButton.CheckedChanged += Location_CheckedChanged;
                    RegionLocation.Children.Add(locationRadioButton);
                }
            }
            BindingContext = this;
        }

        public ListViewPage(Object Types, int index, string Type)
        {
            InitializeComponent();
            if (index == 1)
            {
                List<ObjectTypeClass> objectTypeClasses = (List<ObjectTypeClass>)Types;
                ListType = 1;
                Title = "Выберете тип объекта";
                RegionLocationFrame.IsVisible = false;
                foreach (var type in objectTypeClasses)
                {
                    RadioButton ObjectTypeRadioButton = new RadioButton()
                    {
                        Content = type.Name,
                        Value = type.Name,
                        BindingContext = type,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (type.Name == Type)
                    {
                        ObjectTypeRadioButton.IsChecked = true;
                        SelectedObjectType = type;
                    }
                    ObjectTypeRadioButton.CheckedChanged += AnyField_CheckedChanged;
                    anyField.Children.Add(ObjectTypeRadioButton);
                }
            }
            else if (index == 3)
            {
                List<BaseItemClass> _Types = (List<BaseItemClass>)Types;
                ListType = 3;
                Title = "Выберете тип соединения";
                RegionLocationFrame.IsVisible = false;
                foreach (var type in _Types)
                {
                    RadioButton ConnectionTypeRadioButton = new RadioButton()
                    {
                        Content = type.Name,
                        Value = type.Name,
                        BindingContext = type,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (type.Name == Type)
                    {
                        ConnectionTypeRadioButton.IsChecked = true;
                        SelectedItemFromAnyField = type;
                    }
                    ConnectionTypeRadioButton.CheckedChanged += AnyField_CheckedChanged;
                    anyField.Children.Add(ConnectionTypeRadioButton);
                }
            }
            else if (index == 4)
            {
                List<BaseItemClass> _Types = (List<BaseItemClass>)Types;
                ListType = 4;
                Title = "Выберете тип ПУ";
                RegionLocationFrame.IsVisible = false;
                foreach (var type in _Types)
                {
                    RadioButton PUTypeRadioButton = new RadioButton()
                    {
                        Content = type.Name,
                        Value = type.Name,
                        BindingContext = type,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (type.Name == Type)
                    {
                        PUTypeRadioButton.IsChecked = true;
                        SelectedItemFromAnyField = type;
                    }
                    else if (type.Name == "ART-05 (uni)")
                    {
                        PUTypeRadioButton.IsChecked = true;
                        SelectedItemFromAnyField = type;
                    }
                    PUTypeRadioButton.CheckedChanged += AnyField_CheckedChanged;
                    anyField.Children.Add(PUTypeRadioButton);
                }
            }
            else if (index == 5)
            {
                List<BaseItemClass> _Types = (List<BaseItemClass>)Types;
                ListType = 5;
                Title = "Выберете тип учета";
                RegionLocationFrame.IsVisible = false;
                foreach (var type in _Types)
                {
                    RadioButton AccountingTypeRadioButton = new RadioButton()
                    {
                        Content = type.Name,
                        Value = type.Name,
                        BindingContext = type,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (type.Name == Type)
                    {
                        AccountingTypeRadioButton.IsChecked = true;
                        SelectedItemFromAnyField = type;
                    }
                    AccountingTypeRadioButton.CheckedChanged += AnyField_CheckedChanged;
                    anyField.Children.Add(AccountingTypeRadioButton);
                }
            }
            else if (index == 6)
            {
                List<BaseItemClass> _Types = (List<BaseItemClass>)Types;
                ListType = 6;
                Title = "Выберете среду учета";
                RegionLocationFrame.IsVisible = false;
                foreach (var type in _Types)
                {
                    RadioButton EnvironmentTypeRadioButton = new RadioButton()
                    {
                        Content = type.Name,
                        Value = type.Name,
                        BindingContext = type,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (type.Name == Type)
                    {
                        EnvironmentTypeRadioButton.IsChecked = true;
                        SelectedItemFromAnyField = type;
                    }
                    EnvironmentTypeRadioButton.CheckedChanged += AnyField_CheckedChanged;
                    anyField.Children.Add(EnvironmentTypeRadioButton);
                }
            }
            else if (index == 7)
            {
                List<BaseItemClass> _Types = (List<BaseItemClass>)Types;
                ListType = 7;
                Title = "Выберете состояние";
                RegionLocationFrame.IsVisible = false;
                foreach (var type in _Types)
                {
                    RadioButton CounterStateRadioButton = new RadioButton()
                    {
                        Content = type.Name,
                        Value = type.Name,
                        BindingContext = type,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (type.Name == Type)
                    {
                        CounterStateRadioButton.IsChecked = true;
                        SelectedItemFromAnyField = type;
                    }
                    CounterStateRadioButton.CheckedChanged += AnyField_CheckedChanged;
                    anyField.Children.Add(CounterStateRadioButton);
                }
            }
            else if (index == 9)
            {
                List<BaseItemClass> _Types = (List<BaseItemClass>)Types;
                ListType = 9;
                Title = "Выберете тип контроллера";
                RegionLocationFrame.IsVisible = false;
                foreach (var type in _Types)
                {
                    RadioButton CounterTypeRadioButton = new RadioButton()
                    {
                        Content = type.Name,
                        Value = type.Name,
                        BindingContext = type,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (type.Name == Type)
                    {
                        CounterTypeRadioButton.IsChecked = true;
                        SelectedItemFromAnyField = type;
                    }
                    CounterTypeRadioButton.CheckedChanged += AnyField_CheckedChanged;
                    anyField.Children.Add(CounterTypeRadioButton);
                }
            }
            else if (index == 10)
            {
                ListType = 10;
                Title = "Выберете тип архива";
                RegionLocationFrame.IsVisible = false;
                foreach (var type in ArchiveTypeList)
                {
                    RadioButton ArchiveTypeRadioButton = new RadioButton()
                    {
                        Content = type.Name,
                        Value = type.Name,
                        BindingContext = type,
                        Margin = new Thickness(4, 4),
                        FontSize = 14
                    };
                    if (type.Name == Type)
                    {
                        ArchiveTypeRadioButton.IsChecked = true;
                        SelectedItemFromAnyField = type;
                    }
                    ArchiveTypeRadioButton.CheckedChanged += AnyField_CheckedChanged;
                    anyField.Children.Add(ArchiveTypeRadioButton);
                }
            }
            anyFieldFrame.IsVisible = true;
            anyField.IsVisible = true;
            BindingContext = this;
        }

        public ListViewPage(List<ParentedContactClass> Consumers, int index, string consumerName)
        {
            InitializeComponent();
            ListType = index;
            Title = "Выберете потребителя";
            RegionLocationFrame.IsVisible = false;
            foreach (var consumer in Consumers)
            {
                RadioButton ConsumerRadioButton = new RadioButton()
                {
                    Content = consumer.Name,
                    Value = consumer.Name,
                    BindingContext = consumer,
                    Margin = new Thickness(4, 4),
                    FontSize = 14
                };
                if (consumer.Name == consumerName)
                {
                    ConsumerRadioButton.IsChecked = true;
                    SelectedConsumer = consumer;
                }

                ConsumerRadioButton.CheckedChanged += AnyField_CheckedChanged;
                anyField.Children.Add(ConsumerRadioButton);
            }
            anyFieldFrame.IsVisible = true;
            anyField.IsVisible = true;
            BindingContext = this;
        }

        public ListViewPage(List<string> speedList, int index, string speedText)
        {
            InitializeComponent();
            ListType = index;
            Title = "Выберете скорость";
            RegionLocationFrame.IsVisible = false;
            foreach (string speed in speedList)
            {
                RadioButton SpeedRadioButton = new RadioButton()
                {
                    Content = speed,
                    Value = speed,
                    BindingContext = speed,
                    Margin = new Thickness(4, 4),
                    FontSize = 14
                };
                if (speed == speedText)
                {
                    SpeedRadioButton.IsChecked = true;
                    SelectedSpeed = speed;
                }
                SpeedRadioButton.CheckedChanged += AnyField_CheckedChanged;
                anyField.Children.Add(SpeedRadioButton);
            }
            anyFieldFrame.IsVisible = true;
            anyField.IsVisible = true;
            BindingContext = this;
        }

        private void AnyField_CheckedChanged(object sender, EventArgs e)
        {
            switch (ListType)
            {
                case 1:
                    RadioButton radioButtonType = sender as RadioButton;
                    SelectedObjectType = radioButtonType.BindingContext as ObjectTypeClass;
                    break;
                case 2:
                    RadioButton radioButtonConsumer = sender as RadioButton;
                    SelectedConsumer = radioButtonConsumer.BindingContext as ParentedContactClass;
                    break;
                case 3:
                    RadioButton radioButtonConnectionType = sender as RadioButton;
                    SelectedItemFromAnyField = radioButtonConnectionType.BindingContext as BaseItemClass;
                    break;
                case 4:
                    RadioButton radioButtonPUType = sender as RadioButton;
                    SelectedItemFromAnyField = radioButtonPUType.BindingContext as BaseItemClass;
                    break;
                case 5:
                    RadioButton radioButtonAccountingType = sender as RadioButton;
                    SelectedItemFromAnyField = radioButtonAccountingType.BindingContext as BaseItemClass;
                    break;
                case 6:
                    RadioButton radioButtonEnvironment = sender as RadioButton;
                    SelectedItemFromAnyField = radioButtonEnvironment.BindingContext as BaseItemClass;
                    break;
                case 7:
                    RadioButton radioButtonCounterState = sender as RadioButton;
                    SelectedItemFromAnyField = radioButtonCounterState.BindingContext as BaseItemClass;
                    break;
                case 8:
                    RadioButton radioButtonSpeed = sender as RadioButton;
                    SelectedSpeed = (string)radioButtonSpeed.BindingContext;
                    break;
                case 9:
                    RadioButton radioButtonCounterType = sender as RadioButton;
                    SelectedItemFromAnyField = radioButtonCounterType.BindingContext as BaseItemClass;
                    break;
                case 10:
                    RadioButton radioButtonArchiveType = sender as RadioButton;
                    SelectedItemFromAnyField = radioButtonArchiveType.BindingContext as BaseItemClass;
                    break;
            }
        }

        private async void AcceptButtonClicked(object sender, EventArgs e)
        {
            switch (ListType)
            {
                case 0:
                    MessagingCenter.Send(this, "SetLocation", SelectedLocation);
                    await Navigation.PopModalAsync(true);
                    break;
                case 1:
                    MessagingCenter.Send(this, "SetObjectType", SelectedObjectType);
                    await Navigation.PopModalAsync(true);
                    break;
                case 2:
                    MessagingCenter.Send(this, "SetConsumer", SelectedConsumer);
                    await Navigation.PopModalAsync(true);
                    break;
                case 3:
                    MessagingCenter.Send(this, "SetConnectionType", SelectedItemFromAnyField);
                    await Navigation.PopModalAsync(true);
                    break;
                case 4:
                    MessagingCenter.Send(this, "SetPuType", SelectedItemFromAnyField);
                    await Navigation.PopModalAsync(true);
                    break;
                case 5:
                    MessagingCenter.Send(this, "SetAccounting", SelectedItemFromAnyField);
                    await Navigation.PopModalAsync(true);
                    break;
                case 6:
                    MessagingCenter.Send(this, "SetEnvironment", SelectedItemFromAnyField);
                    await Navigation.PopModalAsync(true);
                    break;
                case 7:
                    MessagingCenter.Send(this, "SetCounterState", SelectedItemFromAnyField);
                    await Navigation.PopModalAsync(true);
                    break;
                case 8:
                    MessagingCenter.Send(this, "SetSpeed", SelectedSpeed);
                    await Navigation.PopModalAsync(true);
                    break;
                case 9:
                    MessagingCenter.Send(this, "SetCounterType", SelectedItemFromAnyField);
                    await Navigation.PopModalAsync(true);
                    break;
                case 10:
                    MessagingCenter.Send(this, "SetArchiveType", SelectedItemFromAnyField);
                    await Navigation.PopModalAsync(true);
                    break;
            }
        }

        private void Location_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButtonLocation = sender as RadioButton;
            SelectedLocation = radioButtonLocation.BindingContext as CustomLocationClass;
        }

        private async void DeclineButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}