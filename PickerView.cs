using System;
using System.Collections.Generic;
using Cirrious.FluentLayouts.Touch;
using CoreAnimation;
using UIKit;

namespace PickerView
{
    public class PickerView : UIViewController
    {
        public PickerView() { }


        UITextField SelectGenderTextField = new UITextField();
        UIPickerView GenderPicker = new UIPickerView();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            AddTextField();
            GenderPickerView();


            Constraint();
        }

        private void AddTextField()
        {
            SelectGenderTextField.Placeholder = "Select Gender";
            SelectGenderTextField.Layer.BorderWidth = 1;
            SelectGenderTextField.Layer.BorderColor = UIColor.Black.CGColor;
            SelectGenderTextField.Layer.MasksToBounds = true;
            SelectGenderTextField.Layer.SublayerTransform = CATransform3D.MakeTranslation(5, 0, 0); //to Create a Space At The beginning of the text field


            SelectGenderTextField.InputView = GenderPicker; //To Start The UIPickerView from The bottom.

        }

        private void GenderPickerView()
        {
            var genderList = new List<string> {
           "Male","Female"
        };

            var picker = new GenderPickerModel(genderList);

            GenderPicker.Model = picker;

            picker.ValueChanged += (sender, e) => {

                SelectGenderTextField.Text = picker.SelectedGenderByUser; //Update The Selected Value In the TextField

                View.EndEditing(true);// To Dismiss the Picker View Once The User Select The Value
            };

        }

        // Used Cirrious.FluentLayouts.Touch For Constraints
        private void Constraint()
        {
            View.AddSubviews(SelectGenderTextField);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(


                SelectGenderTextField.WithRelativeWidth(View, 0.80f),
                SelectGenderTextField.WithRelativeHeight(View, 0.05f),
                SelectGenderTextField.WithSameCenterX(View),
                SelectGenderTextField.WithSameCenterY(View)
                

                );
        }
    }

    class GenderPickerModel : UIPickerViewModel
    {
        public EventHandler ValueChanged;
        public string SelectedGenderByUser;
        private List<string> genderList;

        public GenderPickerModel(List<string> genderList)
        {
            this.genderList = genderList;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return genderList.Count;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return genderList[(int)row];
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var gender = genderList[(int)row];
            SelectedGenderByUser = gender;
            ValueChanged(null,null);
        }


    }
}
