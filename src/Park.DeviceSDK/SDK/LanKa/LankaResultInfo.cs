using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.DeviceSDK.LanKa
{
	public class LankaResultInfo {
		public string[] resutList;

		private string _deviceId = string.Empty;

		public string DeviceId {
			get {
				if (string.IsNullOrEmpty(_deviceId)) {
					_deviceId = ImgName.Split('_')[0];
				}
				return _deviceId;
			}
		}
		public string CarNumber { get;private set; }

		public string ImgName { get; private set; }


		public LankaResultInfo(string result) {
			resutList = result.Split(',');
			CarNumber = resutList[5].Contains( "\0") ? "" : resutList[5];
			ImgName = resutList[1];
		}

	}
}
