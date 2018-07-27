using Park.Devices.Models;
using Park.ParkBox;
using Park.Devices.Interfaces;
using Park.Parks.ParkBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Park.Enum;

namespace Park.Parks.ParkBox.Core
{
    public class LedManager:ISingletonDependency
    {

        private readonly IParkBoxOptions parkBoxOptions;

        private Dictionary<long, List<ILedable>> entranceLedCache;


        public LedManager(IParkBoxOptions parkBoxOptions) {
            this.parkBoxOptions = parkBoxOptions;
            entranceLedCache = new Dictionary<long, List<ILedable>>();
        }

        private List<ILedable> GetLedables(Devices.Interfaces.IDeviceable deviceable)
        {
            List<ILedable> temp = null;
            if (entranceLedCache.Keys.Any(x => x == deviceable.EntranceDto.Id))
            {
                temp = entranceLedCache[deviceable.EntranceDto.Id];
            }
            else
            {
                if (parkBoxOptions.DeciceInfos.Any(x => x.EntranceDto.Id == deviceable.EntranceDto.Id && x.DeviceType == Enum.DeviceType.Led))
                {
                    temp = Array.ConvertAll<DeviceInfoDto, ILedable>(parkBoxOptions.DeciceInfos.Where(x => x.EntranceDto.Id == deviceable.EntranceDto.Id && x.DeviceType == Enum.DeviceType.Led).ToArray(),
                        x => { return x.Controlable.Led; }).ToList();
                }
                else
                {
                    temp = null;
                }
                if (!entranceLedCache.ContainsKey(deviceable.EntranceDto.Id))
                {
                    entranceLedCache.Add(deviceable.EntranceDto.Id, temp);
                }
            }
            return temp;
        }

        public async Task SpeakAndShowText(Devices.Interfaces.IDeviceable deviceable, CarInModel carIn, PermissionResult permissionResult)
        {

            var temp = GetLedables(deviceable);

            await Task.Run(() => temp?.ForEach(x => {
                x.Speak(string.Empty);
                x.Show(string.Empty);
            }));
        }


        public async Task SpeakAndShowText(Devices.Interfaces.IDeviceable deviceable, CarOutModel carOut, OutEnum outEnum)
        {
            if (deviceable == null)
                return;
            var temp = GetLedables(deviceable);

            await Task.Run(() => temp?.ForEach(x =>
            {
                x.Speak(string.Empty);
                x.Show(string.Empty);
            }));
        }




    }
}
