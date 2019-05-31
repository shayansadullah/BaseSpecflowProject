namespace BaseSolution.Step_Definitions
{
    using BaseSolution.Pages;
    using OpenQA.Selenium;
    using TechTalk.SpecFlow;
    using NUnit.Framework;

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

        [StepDefinition(@"I save the Extraction Rule")]
        public void Save()
        {
            Page.WaitForElementById("saveButton");
            Page.ClickById("saveButton");
            Page.WaitForElementById("addExtractionRuleLink");
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
            var bar = Page.GetTextByXPath("//td[contains(text(), 'Any Rule Name')]");
            Assert.That(bar, Is.EqualTo("Any Rule Name"));

        }
    }
}