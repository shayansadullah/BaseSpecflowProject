namespace BaseSolution.Step_Definitions
{
    using System;
    using BaseSolution.Pages;
    using OpenQA.Selenium;
    using TechTalk.SpecFlow;
    using NUnit.Framework;
    using System.Collections.Generic;
    using Constants;

    [Binding]
    internal class CommonSteps
    {
        public CommonSteps(SUTMainPage page)
        {
            Page = page;
        }

        private SUTMainPage Page { get; set; }
        private string ExtractionName { get; set; }

        [StepDefinition(@"I have navigated to '(.*)'")]
        public void GivenIHaveNavigatedTo(string website)
        {
            Page.NavigateTo(website);
        }

        [StepDefinition(@"I click on '(.*)' link")]
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

        [StepDefinition(@"I enter the Add Extraction Rule field '(.*)' with the text '(.*)'")]
        [StepDefinition(@"I update the Add Extraction Rule field '(.*)' with the drop-down value of '(.*)'")]
        [StepDefinition(@"I update the Add Extraction Rule field '(.*)' with value '(.*)'")]
        [StepDefinition(@"I update the Add Extraction Rule field '(.*)' with values '(.*)'")]
        public void AddExtractionDataField(string fieldName, string formValue)
        {
            Page.WaitForElementById("addEditRuleDialog");
            switch (fieldName)
            {
                case "Name":
                    {
                        var dateTime = DateTime.Now.ToString("MMddyyyyhhmmss");
                        var nameField = string.Format(formValue + "-" + dateTime); 
                        this.ExtractionName = nameField;
                        Page.EnterTextIntoTextBox(By.Id(IdAttribute.NameInput), nameField);
                        break;
                    }
                case "SearchName":
                    {
                        this.ExtractionName = formValue;
                        Page.EnterTextIntoTextBox(By.Id("nameInput"), formValue);
                        break;
                    }
                case "Type":
                    {
                        switch (formValue)
                        {
                            case "Content Pattern Match":
                                {
                                    Page.ClickById("contentPatternTypeOption");
                                    break;
                                }
                        }
                        break;
                    }
                case "Data Type":
                    {
                        Page.SelectDropDownItemByOption(By.Id("ruleDataTypeList"), formValue);
                        break;
                    }
                case "Description":
                    {
                        Page.EnterTextIntoTextBox(By.Id("descriptionInput"), formValue);
                        break;
                    }
                case "Masking":
                    {
                        ClickOnMaskingCheckBox();
                        var maskitem = formValue.Split(',');
                        PartToMask(maskitem[0]);
                        MaskSize(maskitem[1]);
                        break;
                    }
                case "Pattern":
                    {
                        Page.EnterTextIntoTextBox(By.Id("expressionInput"), formValue);
                        break;
                    }
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

        [StepDefinition(@"I save the Extraction Rule successfully")]
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

        [StepDefinition(@"I search for the Extraction Rule")]
        public void SearchForExtractionRule()
        {
            Page.WaitForElementById("gs_name");
            Page.EnterTextIntoTextBox(By.Id("gs_name"), this.ExtractionName);
            Page.SendEnterOrReturnKey(By.Id("gs_name"));
        }

        [StepDefinition(@"Extraction Rule is present")]
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

        [StepDefinition(@"Remove Extraction")]
        public void RemoveExtractionFile()
        {

        }
    }
}