using Microsoft.AspNetCore.Mvc.Testing;
using N5Test.Models.PermissionTypes;
using System.Net.Http.Headers;
using System.Text.Json;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace N5Test.IntegrationTest.Controlller.PermissionType
{
    public class PermissionTypeApiTest :BaseTest, IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient _client;

        public PermissionTypeApiTest(WebApplicationFactory<Program> application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task ShouldPostPermissionTypeAsync() {
            //given
            PermissionTypeDTO permissionTypeDTO = CreatePermissionTypeDTOFiller().Create();
            permissionTypeDTO.Id = 0;
            
            using var jsonContent = new StringContent(JsonSerializer.Serialize(permissionTypeDTO));
            jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            //when
            HttpResponseMessage response = await _client.PostAsync("/api/v1/PermisionTypes", jsonContent);

            //then
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
