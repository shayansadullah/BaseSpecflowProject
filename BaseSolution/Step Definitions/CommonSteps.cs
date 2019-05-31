namespace BaseSolution.Step_Definitions
{
    using System;
    using BaseSolution.Pages;
    using OpenQA.Selenium;
    using TechTalk.SpecFlow;
    using NUnit.Framework;
    using TechTalk.SpecFlow;
    using System.Collections.Generic;

    [Binding]
    internal class CommonSteps
    {
        public CommonSteps(SUTMainPage page)
        {
            Page = page;
        }

        private SUTMainPage Page { get; set; }


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
                        Page.ClickById("maskingCheckbox");
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

        [StepDefinition(@"I save the Extraction Rule without any data")]
        public void SaveExtractionRules()
        {
            Page.WaitForElementById("saveButton");
            Page.ClickById("saveButton");
        }

        [StepDefinition(@"I search for the Extraction Rule '(.*)'")]
        public void SearchForExtractionRule(string ruleName)
        {
            Page.WaitForElementById("gs_name");
            Page.EnterTextIntoTextBox(By.Id("gs_name"), ruleName);
            Page.SendEnterOrReturnKey(By.Id("gs_name"));
        }

        [StepDefinition(@"Extraction Rule (.*) is present")]
        public void ThenIAmDisplayedDetailsFor(string extractionRuleText)
        {
            var extractionName = Page.GetTextByXPath("//td[contains(text(), 'Any Rule Name')]");
            Assert.That(extractionName, Is.EqualTo("Any Rule Name"), string.Format("Extraction Name '%s' is not present", extractionName));
        }

        [StepDefinition(@"I should get an error message for the following fields:")]
        public void GetFormInputErrors(Table table)
        {
            var fieldsWithErrors = Page.GetElementsByXPath("//label[@title='This field is required']/..");
            string textConcatenate = null;
            foreach (var fieldWithError in fieldsWithErrors)
            {
                textConcatenate += fieldWithError.Text;
            }

            string fieldsWithoutErrors = null;
            foreach(var row in table.Rows)
            {
                if (!textConcatenate.Contains(row[0]))
                {
                    fieldsWithoutErrors += row[0];
                }
            }

            if (fieldsWithoutErrors != null)
            {
                Assert.Fail(string.Format("Field input errors not reported for: %s", fieldsWithoutErrors));
            }
            
        }
    }
}