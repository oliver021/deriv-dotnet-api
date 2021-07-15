namespace OliWorkshop.Deriv.ApiRequests
{
    /// <summary>
    /// [Optional] If you specify this field, only symbols available for trading by that landing
    /// company will be returned. If you are logged in, only symbols available for trading by
    /// your landing company will be returned regardless of what you specify in this field.
    /// </summary>
    public enum LandingCompany { Champion, ChampionVirtual, Iom, Malta, Maltainvest, Svg, Vanuatu, Virtual };
}
