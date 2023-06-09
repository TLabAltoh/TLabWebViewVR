﻿/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using Meta.WitAi.Configuration;
using Meta.WitAi.Data.Configuration;

namespace Meta.WitAi.Interfaces
{
    public interface IWitRequestProvider
    {
        WitRequest CreateWitRequest(WitConfiguration config, WitRequestOptions requestOptions, IDynamicEntitiesProvider[] additionalEntityProviders = null);
    }
}
