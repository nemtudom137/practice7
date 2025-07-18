using OpenQA.Selenium;

namespace Business.ApplicationInterface.Sections;

public static class Header
{
    public static readonly By Services = By.XPath("//header//a[text()='Services']");
    public static readonly By Insights = By.XPath("//header//a[text()='Insights']");
    public static readonly By About = By.XPath("//header//a[text()='About']");
    public static readonly By Carriers = By.XPath("//header//a[text()='Careers']");
    public static readonly By SearchIcon = By.XPath("//button[contains(@class,'header-search')]");
    public static readonly By SearchInput = By.CssSelector("input#new_form_search");
    public static readonly By FindButton = By.XPath("//div[contains(@class,'search-results__input-holder')]/following-sibling::button");

    public static By DropdownMenuItem(string linkText) => By.XPath($"//ul[@class='top-navigation__row']//li[contains(@class,'js-opened')]//a[normalize-space(text())='{linkText}']");
}