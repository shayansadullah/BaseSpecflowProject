﻿namespace BaseSolution.Step_Definitions
{
    using System;
    using BaseSolution.Pages;
    using OpenQA.Selenium;
    using TechTalk.SpecFlow;
    using NUnit.Framework;
    using System.Collections.Generic;
    using Constants;

    [Binding]
    internal class CommonSteps : Steps
    {
        public CommonSteps(SUTMainPage page)
        {
            Page = page;
        }

        private SUTMainPage Page { get; set; }
        private string ExtractionName { get; set; }

        [StepDefinition(@"the user navigates to the '(.*)' site")]
        public void GivenIHaveNavigatedTo(string page)
        {
            Page.NavigateTo(page);
        }

        [StepDefinition(@"the user opens the '(.*)' section")]

        public void ClickOnLink(string linkName)
        {
            switch (linkName)
            {
                case "Add Extraction Rule":
                    {
                        Page.WaitForElementById("addExtractionRuleLink");
                        Page.ClickById("addExtractionRuleLink");
                        break;
                    }
            }
        }

        [StepDefinition(@"the User updates the Add Extraction Rule field '(.*)' with the text '(.*)'")]
        [StepDefinition(@"the User updates the Add Extraction Rule field '(.*)' with the drop-down value of '(.*)'")]
        [StepDefinition(@"the User updates the Add Extraction Rule field '(.*)' with value '(.*)'")]
        [StepDefinition(@"the User updates the Add Extraction Rule field '(.*)' with values '(.*)'")]
        public void AddExtractionDataField(string fieldName, string formValue)
        {
            Page.WaitForElementById("addEditRuleDialog");
            switch (fieldName)
            {
                case "Name":
                        var dateTime = DateTime.Now.ToString("MMddyyyyhhmmss");
                        var nameField = string.Format(formValue + "-" + dateTime); 
                        this.ExtractionName = nameField;
                        Page.EnterTextIntoTextBox(By.Id(IdAttribute.NameInput), nameField);
                        break;
                case "SearchName":
                        this.ExtractionName = formValue;
                        Page.EnterTextIntoTextBox(By.Id("nameInput"), formValue);
                        break;
                case "Type":
                        switch (formValue)
                        {
                            case "Content Pattern Match":
                                    Page.ClickById("contentPatternTypeOption");
                                    break;
                        }
                        break;
                case "Data Type":
                        Page.SelectDropDownItemByOption(By.Id("ruleDataTypeList"), formValue);
                        break;
                case "Description":
                        Page.EnterTextIntoTextBox(By.Id("descriptionInput"), formValue);
                        break;
                case "PartToMask":
                        ClickOnMaskingCheckBox();
                        PartToMask(formValue);
                        break;
                case "MaskSize":
                        MaskSize(formValue);
                        break;
                case "Pattern":
                        Page.EnterTextIntoTextBox(By.Id("expressionInput"), formValue);
                        break;
                default:
                        Assert.Fail(string.Format("Field is not recognised: {0}", fieldName));
                        break;
            }
        }

        [Given(@"the User updates the Extraction Rule with (.*),(.*),(.*),(.*),(.*),(.*),(.*)")]
        public void AddExtractionData(string name, 
                                      string type, 
                                      string dataType, 
                                      string description,
                                      string partToMask,
                                      string maskSize,
                                      string patternValue)
        {
            Given(string.Format("the User updates the Add Extraction Rule field 'Name' with the text '{0}'", name));
            Given(string.Format("the User updates the Add Extraction Rule field 'Type' with the drop-down value of '{0}'", type));
            Given(string.Format("the User updates the Add Extraction Rule field 'Data Type' with the drop-down value of '{0}'", dataType));
            Given(string.Format("the User updates the Add Extraction Rule field 'Description' with the text '{0}'", description));
            Given(string.Format("the User updates the Add Extraction Rule field 'PartToMask' with values '{0}'", partToMask));
            Given(string.Format("the User updates the Add Extraction Rule field 'MaskSize' with values '{0}'", maskSize));
            Given(string.Format("the User updates the Add Extraction Rule field 'Pattern' with the text '{0}'", patternValue));
        }

        [StepDefinition(@"I update the Masking field:")]
        public void updateTheMaskingField(Table maskingFields)
        {
            foreach(var row in maskingFields.Rows)
            {
                Page.ClearText("maskPercentInput");
                AddExtractionDataField("PartToMask", row[0]);
                AddExtractionDataField("MaskSize", row[1]);
                SaveExtractionRules();
                GetValidationInputError("Mask size (%)", row[2]);
            }
        }

        [StepDefinition(@"I click on the masking checkbox")]
        internal void ClickOnMaskingCheckBox()
        {
            var stateOfCheckBox = Page.IsCheckboxSelected(By.Id("maskingCheckbox"));
            if (!stateOfCheckBox)
            {
                Page.ClickById("maskingCheckbox");
            }
        }

        private void PartToMask(string partToMask)
        {
            Page.WaitForElementById("maskingPartList");
            Page.SelectDropDownItemByOption(By.Id("maskingPartList"), partToMask);
        }

        private void MaskSize(string percentInput)
        {
            Page.WaitForElementById("maskPercentInput");
            Page.EnterTextIntoTextBox(By.Id("maskPercentInput"), percentInput);
        }

        [StepDefinition(@"that User saves the Extraction Rule")]
        public void Save()
        {
            SaveExtractionRules();
            Page.WaitForElementById("addExtractionRuleLink");
        }

        [StepDefinition(@"I save the Extraction Rule")]
        [StepDefinition(@"I save the Extraction Rule without any data input")]
        public void SaveExtractionRules()
        {
            Page.WaitForElementById("saveButton");
            Page.ClickById("saveButton");
        }

        [StepDefinition(@"that User performs a search for that Extraction Rule")]
        public void SearchForExtractionRule()
        {
            Page.WaitForElementById("gs_name");
            Page.EnterTextIntoTextBox(By.Id("gs_name"), this.ExtractionName);
            Page.SendEnterOrReturnKey(By.Id("gs_name"));
        }

        [StepDefinition(@"the Extraction Rule created is present")]
        public void ThenIAmDisplayedDetailsFor()
        {
            var extractionName = Page.GetTextByXPath(string.Format("//td[contains(text(), '{0}')]", this.ExtractionName));
            Assert.That(extractionName, Is.EqualTo(this.ExtractionName), string.Format("Extraction Name '{0}' is not present", extractionName));
        }

        [StepDefinition(@"I should get an error message stating that the fields are required for the following:")]
        public void GetFormInputErrors(Table table)
        {
            var fieldsWithErrors = Page.GetElementsByXPath("//label[@title='This field is required']/..");
            GetFormInputErrorDetails(fieldsWithErrors, table);
        }

        [StepDefinition(@"I should get an error message for the following fields:")]
        public void GetMaskInputErrorsTable(Table table)
        {
            foreach(var row in table.Rows)
            {
                GetValidationInputError(row[0], row[1]);
            }
        }

        [StepDefinition(@"the field (.*) has the error value: (.*)")]
         public void GetValidationInputError(string field, string errorValue)
        {
            var myField = Page.GetElementByXPath(string.Format("//label[@title='{0}']/../..", errorValue));
            Assert.That(myField.Text.Contains(field),
                string.Format("Validation error is not present for field: '{0}'", field));
        }

        /// <summary>
        /// Get form Input Error Details as a concatenated string 
        /// </summary>
        /// <param name="fieldsWithErrors"></param>
        /// <param name="table"></param>
        public void GetFormInputErrorDetails(IReadOnlyCollection<IWebElement> fieldsWithErrors, Table table)
        {
            string textConcatenate = null;
            foreach (var fieldWithError in fieldsWithErrors)
            {
                textConcatenate += fieldWithError.Text;
            }
            string fieldsThatShouldHaveHadErrorsShown = null;
            foreach (var row in table.Rows)
            {
                if (!textConcatenate.Contains(row[0]))
                {
                    fieldsThatShouldHaveHadErrorsShown += row[0] + ", ";
                }
            }
            if (fieldsThatShouldHaveHadErrorsShown != null)
            {
                Assert.Fail(string.Format("Field input validation errors not reported for the following fields: '{0}'", fieldsThatShouldHaveHadErrorsShown));
            }
        }
    }
}