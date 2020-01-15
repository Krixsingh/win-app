﻿/*
 * Copyright (c) 2020 Proton Technologies AG
 *
 * This file is part of ProtonVPN.
 *
 * ProtonVPN is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ProtonVPN is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with ProtonVPN.  If not, see <https://www.gnu.org/licenses/>.
 */

using ProtonVPN.Common;
using ProtonVPN.Core.Settings;
using ProtonVPN.Service.Contract.Settings;
using System.Linq;

namespace ProtonVPN.Core.Service.Settings
{
    public class SettingsContractProvider
    {
        private readonly IAppSettings _appSettings;

        public SettingsContractProvider(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public SettingsContract GetSettingsContract()
        {
            return new SettingsContract
            {
                KillSwitch = new KillSwitchSettingsContract
                {
                    Enabled = _appSettings.KillSwitch
                },
                SplitTunnel = new SplitTunnelSettingsContract
                {
                    Mode = _appSettings.SplitTunnelingEnabled ? _appSettings.SplitTunnelMode : SplitTunnelMode.Disabled,
                    AppPaths = _appSettings.GetSplitTunnelApps(),
                    Ips = _appSettings.SplitTunnelingIps.Where(i => i.Enabled).Select(i => i.Ip).ToArray()
                },
                Ipv6LeakProtection = _appSettings.Ipv6LeakProtection,
            };
        }
    }
}