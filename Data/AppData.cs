﻿using FishyFlip;

namespace BlazorBsky.Data
{
    public static class AppData
    {
        public static readonly ATProtocol Client = new FishyFlip.ATProtocolBuilder().WithInstanceUrl(new Uri("https://public.api.bsky.app")).Build();
    }
}
