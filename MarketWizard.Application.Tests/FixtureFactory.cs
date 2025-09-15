using AutoFixture;

namespace MarketWizard.Application.Tests;

public static class FixtureFactory
{
    public static Fixture Create()
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    }
}