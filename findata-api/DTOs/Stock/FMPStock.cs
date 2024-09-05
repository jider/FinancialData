﻿using System.Text.Json.Serialization;

namespace findata_api.DTOs.Stock;

public class FMPStock
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("beta")]
    public double Beta { get; set; }

    [JsonPropertyName("volAvg")]
    public int VolAvg { get; set; }

    [JsonPropertyName("mktCap")]
    public long MktCap { get; set; }

    [JsonPropertyName("lastDiv")]
    public double LastDiv { get; set; }

    [JsonPropertyName("range")]
    public string Range { get; set; } = string.Empty;

    [JsonPropertyName("changes")]
    public double Changes { get; set; }

    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("cik")]
    public string Cik { get; set; } = string.Empty;

    [JsonPropertyName("isin")]
    public string Isin { get; set; } = string.Empty;

    [JsonPropertyName("cusip")]
    public string Cusip { get; set; } = string.Empty;

    [JsonPropertyName("exchange")]
    public string Exchange { get; set; } = string.Empty;

    [JsonPropertyName("exchangeShortName")]
    public string ExchangeShortName { get; set; } = string.Empty;

    [JsonPropertyName("industry")]
    public string Industry { get; set; } = string.Empty;

    [JsonPropertyName("website")]
    public string Website { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("ceo")]
    public string Ceo { get; set; } = string.Empty;

    [JsonPropertyName("sector")]
    public string Sector { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("fullTimeEmployees")]
    public string FullTimeEmployees { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("zip")]
    public string Zip { get; set; } = string.Empty;

    [JsonPropertyName("dcfDiff")]
    public double DcfDiff { get; set; }

    [JsonPropertyName("dcf")]
    public double Dcf { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("ipoDate")]
    public string IpoDate { get; set; } = string.Empty;

    [JsonPropertyName("defaultImage")]
    public bool DefaultImage { get; set; }

    [JsonPropertyName("isEtf")]
    public bool IsEtf { get; set; }

    [JsonPropertyName("isActivelyTrading")]
    public bool IsActivelyTrading { get; set; }

    [JsonPropertyName("isAdr")]
    public bool IsAdr { get; set; }

    [JsonPropertyName("isFund")]
    public bool IsFund { get; set; }
}
