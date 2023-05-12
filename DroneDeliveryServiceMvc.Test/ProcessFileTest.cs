namespace DroneDeliveryServiceMvc.Test
{
    public class ProcessFileTest : BaseTester
    {
        [Fact]
        public void ReadAsStringAsyncTest()
        {
            var result = ListStringFile1();

            Assert.Equal(17, result.Count);
            Assert.Equal("[DroneA], [200], [DroneB], [250], [DroneC], [100]", result[0]);
            Assert.Equal("[LocationP], [90]", result[16]);
        }

        [Fact]
        public void CreateListDroneTest1()
        {
            var result = _processFileService.CreateListDrone(ListStringFile1());
            Assert.Equal(3, result.Count);
            Assert.Equal("[DroneA]", result[0].Name);
            Assert.Equal("[200]", result[0].StringMaximumWeight);
            Assert.Equal(200, result[0].MaximumWeight);
            Assert.Equal(1, result[0].Order);
        }

        [Fact]
        public void CreateListLocationTest1()
        {
            var result = _processFileService.CreateListLocation(ListStringFile1());
            Assert.Equal(16, result.Count);
            Assert.Equal("[LocationA]", result[0].Name);
            Assert.Equal("[200]", result[0].StringPackageWeight);
            Assert.Equal(200, result[0].PackageWeight);
        }
    }
}