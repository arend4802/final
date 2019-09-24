using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;

namespace final
{

    public partial class MainWindow : Window
    {
        List<Repair> repairs = new List<Repair>();
        public Status Status { get; set; }




        public MainWindow()
        {
            InitializeComponent();


            repairs.Add(
                new Repair()
                {
                    RepairDescription = "rrgergarer4",
                    StartDatum = "12/2/2019",
                    EndDatum = "24/2/2019",
                    Status = Status.Inbehandeling
                });

            Repair repair = new Repair();

            repair.RepairDescription = "25er4";
            repair.StartDatum = "12/2/2019";
            repair.EndDatum = "24/2/2019";
            repair.Status = Status.WachtOpOnderdelen;

            repairs.Add(repair);



            repairs.Add(new Repair() { RepairDescription = "5242", StartDatum = "12/2/2019", EndDatum = "24/2/2019", Status = Status.Inafwachting });


            ICRepairList.ItemsSource = repairs;
        }



        public void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            Repair repair = new Repair();

            repair.RepairDescription = RepairDescription.Text;
            repair.StartDatum = StartDatum.Text;
            repair.EndDatum = EndDatum.Text;
            repair.Status = Status;
            repairs.Add(repair);

        }

    }
    public class Repair
    {

        public string StartDatum { get; set; }
        public string EndDatum { get; set; }
        public string RepairDescription { get; set; }
        public Status Status { get; set; }

    }
    public enum Status
    {
        Inafwachting,
        Inbehandeling,
        WachtOpOnderdelen,
        Klaar
    }

    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get { return this._enumType; }
            set
            {
                if (value != this._enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }

                    this._enumType = value;
                }
            }
        }

        public EnumBindingSourceExtension() { }

        public EnumBindingSourceExtension(Type enumType)
        {
            this.EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
                throw new InvalidOperationException("The EnumType must be specified.");

            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            Array enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == this._enumType)
                return enumValues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }


}