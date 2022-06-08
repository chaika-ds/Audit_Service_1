using System;
using Tolar.Authenticate.Impl;

namespace AuditService.IntegrationTests.Api;

public class TestSettings: IAuthenticateServiceSettings
{
    string IAuthenticateServiceSettings.Connection => "http://sso-api.netreportservice.xyz/";

    public Guid ServiceId => Guid.Parse("52d8a443-a79d-4331-9cf1-efe3025be599");

    public string ApiKey => "VxxXeFgOqXF3lw4LCNgZsPSjmxRtyCQ8F2UahHBEWRAClWJzyM5xB16d64LP8qrXBg5RchErkurSqudkcREjSYEDYrsx3B40w9yMZkLkzKSZEGapszH5gCIrOUn5BovE";

    public Guid RootNodeId => Guid.Parse("f3213089-6b89-16eb-9430-00000442af32");

    public Guid HallNodeId => Guid.Parse("ceec8e88-c632-4047-8189-7cb3dd2bd027");
}