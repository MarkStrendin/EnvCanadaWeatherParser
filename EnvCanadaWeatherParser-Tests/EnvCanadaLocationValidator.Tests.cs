﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MarkStrendin.EnvCanadaWeatherParser.Tests
{
    public class EnvCanadaLocationValidatorTests
    {
        [Theory(DisplayName = "validateLocationCode should reject invalid location codes")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("abcd")]
        [InlineData("abcdef")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        [InlineData("1234567")]
        [InlineData("a-345")]
        [InlineData("aa-23")]
        [InlineData("90-39")]
        [InlineData("1-2")]
        [InlineData("23-aa")]
        [InlineData("34-sk")]
        [InlineData("sk-341")]
        [InlineData("sk34")]
        [InlineData("sk_34")]        
        public void validateLocationCode_ShouldRejectInvalidLocationCodes(string input)
        {
            Assert.False(EnvCanadaLocationValidator.validateLocationCode(input));
        }

        [Theory(DisplayName = "validateLocationCode should parse valid location codes")]
        [InlineData("ab-1")]
        [InlineData("ab-2")]
        [InlineData("ab-3")]
        [InlineData("ab-4")]
        [InlineData("ab-5")]
        [InlineData("ab-6")]
        [InlineData("ab-7")]
        [InlineData("ab-8")]
        [InlineData("ab-9")]
        [InlineData("ab-10")]
        [InlineData("ab-11")]
        [InlineData("ab-12")]
        [InlineData("ab-14")]
        [InlineData("ab-15")]
        [InlineData("ab-16")]
        [InlineData("ab-17")]
        [InlineData("ab-18")]
        [InlineData("ab-19")]
        [InlineData("ab-20")]
        [InlineData("ab-21")]
        [InlineData("ab-22")]
        [InlineData("ab-23")]
        [InlineData("ab-24")]
        [InlineData("ab-25")]
        [InlineData("ab-26")]
        [InlineData("ab-27")]
        [InlineData("ab-28")]
        [InlineData("ab-29")]
        [InlineData("ab-30")]
        [InlineData("ab-31")]
        [InlineData("ab-32")]
        [InlineData("ab-33")]
        [InlineData("ab-34")]
        [InlineData("ab-35")]
        [InlineData("ab-36")]
        [InlineData("ab-37")]
        [InlineData("ab-38")]
        [InlineData("ab-39")]
        [InlineData("ab-40")]
        [InlineData("ab-41")]
        [InlineData("ab-42")]
        [InlineData("ab-43")]
        [InlineData("ab-44")]
        [InlineData("ab-45")]
        [InlineData("ab-46")]
        [InlineData("ab-47")]
        [InlineData("ab-48")]
        [InlineData("ab-49")]
        [InlineData("ab-50")]
        [InlineData("ab-51")]
        [InlineData("ab-52")]
        [InlineData("ab-53")]
        [InlineData("ab-54")]
        [InlineData("ab-55")]
        [InlineData("ab-56")]
        [InlineData("ab-57")]
        [InlineData("ab-58")]
        [InlineData("ab-59")]
        [InlineData("ab-60")]
        [InlineData("ab-61")]
        [InlineData("ab-62")]
        [InlineData("ab-63")]
        [InlineData("ab-64")]
        [InlineData("ab-65")]
        [InlineData("ab-66")]
        [InlineData("ab-67")]
        [InlineData("ab-68")]
        [InlineData("ab-69")]
        [InlineData("ab-70")]
        [InlineData("ab-71")]
        [InlineData("ab-72")]
        [InlineData("bc-1")]
        [InlineData("bc-3")]
        [InlineData("bc-5")]
        [InlineData("bc-6")]
        [InlineData("bc-7")]
        [InlineData("bc-8")]
        [InlineData("bc-9")]
        [InlineData("bc-10")]
        [InlineData("bc-11")]
        [InlineData("bc-12")]
        [InlineData("bc-13")]
        [InlineData("bc-14")]
        [InlineData("bc-15")]
        [InlineData("bc-16")]
        [InlineData("bc-17")]
        [InlineData("bc-18")]
        [InlineData("bc-19")]
        [InlineData("bc-20")]
        [InlineData("bc-21")]
        [InlineData("bc-22")]
        [InlineData("bc-23")]
        [InlineData("bc-24")]
        [InlineData("bc-25")]
        [InlineData("bc-26")]
        [InlineData("bc-27")]
        [InlineData("bc-28")]
        [InlineData("bc-29")]
        [InlineData("bc-30")]
        [InlineData("bc-31")]
        [InlineData("bc-32")]
        [InlineData("bc-33")]
        [InlineData("bc-34")]
        [InlineData("bc-35")]
        [InlineData("bc-36")]
        [InlineData("bc-37")]
        [InlineData("bc-38")]
        [InlineData("bc-39")]
        [InlineData("bc-40")]
        [InlineData("bc-41")]
        [InlineData("bc-42")]
        [InlineData("bc-43")]
        [InlineData("bc-44")]
        [InlineData("bc-45")]
        [InlineData("bc-46")]
        [InlineData("bc-47")]
        [InlineData("bc-48")]
        [InlineData("bc-49")]
        [InlineData("bc-50")]
        [InlineData("bc-51")]
        [InlineData("bc-52")]
        [InlineData("bc-53")]
        [InlineData("bc-54")]
        [InlineData("bc-55")]
        [InlineData("bc-56")]
        [InlineData("bc-57")]
        [InlineData("bc-58")]
        [InlineData("bc-59")]
        [InlineData("bc-60")]
        [InlineData("bc-61")]
        [InlineData("bc-62")]
        [InlineData("bc-63")]
        [InlineData("bc-64")]
        [InlineData("bc-65")]
        [InlineData("bc-66")]
        [InlineData("bc-67")]
        [InlineData("bc-68")]
        [InlineData("bc-69")]
        [InlineData("bc-70")]
        [InlineData("bc-71")]
        [InlineData("bc-72")]
        [InlineData("bc-73")]
        [InlineData("bc-74")]
        [InlineData("bc-75")]
        [InlineData("bc-76")]
        [InlineData("bc-77")]
        [InlineData("bc-78")]
        [InlineData("bc-79")]
        [InlineData("bc-80")]
        [InlineData("bc-81")]
        [InlineData("bc-82")]
        [InlineData("bc-83")]
        [InlineData("bc-84")]
        [InlineData("bc-85")]
        [InlineData("bc-86")]
        [InlineData("bc-87")]
        [InlineData("bc-88")]
        [InlineData("bc-89")]
        [InlineData("bc-90")]
        [InlineData("bc-91")]
        [InlineData("bc-92")]
        [InlineData("bc-93")]
        [InlineData("bc-94")]
        [InlineData("bc-95")]
        [InlineData("bc-96")]
        [InlineData("bc-97")]
        [InlineData("bc-98")]
        [InlineData("bc-99")]
        [InlineData("bc-100")]
        [InlineData("mb-2")]
        [InlineData("mb-3")]
        [InlineData("mb-4")]
        [InlineData("mb-5")]
        [InlineData("mb-6")]
        [InlineData("mb-7")]
        [InlineData("mb-8")]
        [InlineData("mb-9")]
        [InlineData("mb-10")]
        [InlineData("mb-11")]
        [InlineData("mb-12")]
        [InlineData("mb-13")]
        [InlineData("mb-14")]
        [InlineData("mb-15")]
        [InlineData("mb-16")]
        [InlineData("mb-17")]
        [InlineData("mb-18")]
        [InlineData("mb-19")]
        [InlineData("mb-20")]
        [InlineData("mb-21")]
        [InlineData("mb-22")]
        [InlineData("mb-23")]
        [InlineData("mb-24")]
        [InlineData("mb-25")]
        [InlineData("mb-26")]
        [InlineData("mb-27")]
        [InlineData("mb-28")]
        [InlineData("mb-29")]
        [InlineData("mb-30")]
        [InlineData("mb-31")]
        [InlineData("mb-32")]
        [InlineData("mb-33")]
        [InlineData("mb-34")]
        [InlineData("mb-35")]
        [InlineData("mb-36")]
        [InlineData("mb-37")]
        [InlineData("mb-38")]
        [InlineData("mb-39")]
        [InlineData("mb-40")]
        [InlineData("mb-41")]
        [InlineData("mb-42")]
        [InlineData("mb-43")]
        [InlineData("mb-44")]
        [InlineData("mb-45")]
        [InlineData("mb-46")]
        [InlineData("mb-47")]
        [InlineData("mb-48")]
        [InlineData("mb-49")]
        [InlineData("mb-50")]
        [InlineData("mb-51")]
        [InlineData("mb-52")]
        [InlineData("mb-53")]
        [InlineData("mb-54")]
        [InlineData("mb-55")]
        [InlineData("mb-56")]
        [InlineData("mb-57")]
        [InlineData("mb-58")]
        [InlineData("mb-59")]
        [InlineData("mb-60")]
        [InlineData("mb-61")]
        [InlineData("mb-62")]
        [InlineData("mb-63")]
        [InlineData("mb-64")]
        [InlineData("mb-65")]
        [InlineData("mb-66")]
        [InlineData("mb-67")]
        [InlineData("nb-1")]
        [InlineData("nb-2")]
        [InlineData("nb-3")]
        [InlineData("nb-4")]
        [InlineData("nb-5")]
        [InlineData("nb-6")]
        [InlineData("nb-8")]
        [InlineData("nb-9")]
        [InlineData("nb-10")]
        [InlineData("nb-11")]
        [InlineData("nb-12")]
        [InlineData("nb-13")]
        [InlineData("nb-14")]
        [InlineData("nb-15")]
        [InlineData("nb-16")]
        [InlineData("nb-17")]
        [InlineData("nb-18")]
        [InlineData("nb-19")]
        [InlineData("nb-20")]
        [InlineData("nb-21")]
        [InlineData("nb-22")]
        [InlineData("nb-23")]
        [InlineData("nb-24")]
        [InlineData("nb-25")]
        [InlineData("nb-26")]
        [InlineData("nb-27")]
        [InlineData("nb-28")]
        [InlineData("nb-29")]
        [InlineData("nb-30")]
        [InlineData("nb-31")]
        [InlineData("nb-32")]
        [InlineData("nb-33")]
        [InlineData("nb-34")]
        [InlineData("nb-35")]
        [InlineData("nb-36")]
        [InlineData("nl-1")]
        [InlineData("nl-2")]
        [InlineData("nl-3")]
        [InlineData("nl-4")]
        [InlineData("nl-5")]
        [InlineData("nl-6")]
        [InlineData("nl-7")]
        [InlineData("nl-8")]
        [InlineData("nl-9")]
        [InlineData("nl-10")]
        [InlineData("nl-11")]
        [InlineData("nl-12")]
        [InlineData("nl-13")]
        [InlineData("nl-14")]
        [InlineData("nl-15")]
        [InlineData("nl-16")]
        [InlineData("nl-17")]
        [InlineData("nl-18")]
        [InlineData("nl-19")]
        [InlineData("nl-20")]
        [InlineData("nl-21")]
        [InlineData("nl-22")]
        [InlineData("nl-23")]
        [InlineData("nl-24")]
        [InlineData("nl-25")]
        [InlineData("nl-26")]
        [InlineData("nl-27")]
        [InlineData("nl-28")]
        [InlineData("nl-29")]
        [InlineData("nl-30")]
        [InlineData("nl-31")]
        [InlineData("nl-32")]
        [InlineData("nl-33")]
        [InlineData("nl-34")]
        [InlineData("nl-35")]
        [InlineData("nl-36")]
        [InlineData("nl-37")]
        [InlineData("nl-38")]
        [InlineData("nl-39")]
        [InlineData("nl-40")]
        [InlineData("nl-41")]
        [InlineData("nl-42")]
        [InlineData("nl-43")]
        [InlineData("nl-44")]
        [InlineData("nl-45")]
        [InlineData("nl-46")]
        [InlineData("nl-47")]
        [InlineData("nl-48")]
        [InlineData("nt-1")]
        [InlineData("nt-2")]
        [InlineData("nt-3")]
        [InlineData("nt-4")]
        [InlineData("nt-5")]
        [InlineData("nt-6")]
        [InlineData("nt-7")]
        [InlineData("nt-8")]
        [InlineData("nt-9")]
        [InlineData("nt-10")]
        [InlineData("nt-11")]
        [InlineData("nt-12")]
        [InlineData("nt-13")]
        [InlineData("nt-14")]
        [InlineData("nt-15")]
        [InlineData("nt-16")]
        [InlineData("nt-17")]
        [InlineData("nt-18")]
        [InlineData("nt-19")]
        [InlineData("nt-20")]
        [InlineData("nt-21")]
        [InlineData("nt-22")]
        [InlineData("nt-23")]
        [InlineData("nt-24")]
        [InlineData("nt-26")]
        [InlineData("nt-27")]
        [InlineData("nt-28")]
        [InlineData("nt-29")]
        [InlineData("nt-30")]
        [InlineData("nt-31")]
        [InlineData("ns-1")]
        [InlineData("ns-2")]
        [InlineData("ns-3")]
        [InlineData("ns-4")]
        [InlineData("ns-5")]
        [InlineData("ns-6")]
        [InlineData("ns-7")]
        [InlineData("ns-8")]
        [InlineData("ns-10")]
        [InlineData("ns-11")]
        [InlineData("ns-12")]
        [InlineData("ns-13")]
        [InlineData("ns-14")]
        [InlineData("ns-15")]
        [InlineData("ns-16")]
        [InlineData("ns-17")]
        [InlineData("ns-18")]
        [InlineData("ns-19")]
        [InlineData("ns-20")]
        [InlineData("ns-21")]
        [InlineData("ns-22")]
        [InlineData("ns-23")]
        [InlineData("ns-24")]
        [InlineData("ns-25")]
        [InlineData("ns-26")]
        [InlineData("ns-27")]
        [InlineData("ns-28")]
        [InlineData("ns-29")]
        [InlineData("ns-30")]
        [InlineData("ns-31")]
        [InlineData("ns-32")]
        [InlineData("ns-33")]
        [InlineData("ns-34")]
        [InlineData("ns-35")]
        [InlineData("ns-36")]
        [InlineData("ns-37")]
        [InlineData("ns-38")]
        [InlineData("ns-39")]
        [InlineData("ns-40")]
        [InlineData("ns-41")]
        [InlineData("ns-42")]
        [InlineData("ns-43")]
        [InlineData("ns-44")]
        [InlineData("nu-1")]
        [InlineData("nu-2")]
        [InlineData("nu-3")]
        [InlineData("nu-4")]
        [InlineData("nu-5")]
        [InlineData("nu-6")]
        [InlineData("nu-7")]
        [InlineData("nu-8")]
        [InlineData("nu-9")]
        [InlineData("nu-10")]
        [InlineData("nu-11")]
        [InlineData("nu-12")]
        [InlineData("nu-13")]
        [InlineData("nu-14")]
        [InlineData("nu-15")]
        [InlineData("nu-16")]
        [InlineData("nu-17")]
        [InlineData("nu-18")]
        [InlineData("nu-19")]
        [InlineData("nu-20")]
        [InlineData("nu-21")]
        [InlineData("nu-22")]
        [InlineData("nu-23")]
        [InlineData("nu-24")]
        [InlineData("nu-25")]
        [InlineData("nu-26")]
        [InlineData("nu-27")]
        [InlineData("nu-28")]
        [InlineData("nu-29")]
        [InlineData("on-1")]
        [InlineData("on-3")]
        [InlineData("on-4")]
        [InlineData("on-5")]
        [InlineData("on-7")]
        [InlineData("on-8")]
        [InlineData("on-9")]
        [InlineData("on-11")]
        [InlineData("on-12")]
        [InlineData("on-13")]
        [InlineData("on-14")]
        [InlineData("on-15")]
        [InlineData("on-16")]
        [InlineData("on-17")]
        [InlineData("on-18")]
        [InlineData("on-19")]
        [InlineData("on-21")]
        [InlineData("on-22")]
        [InlineData("on-23")]
        [InlineData("on-24")]
        [InlineData("on-25")]
        [InlineData("on-26")]
        [InlineData("on-27")]
        [InlineData("on-28")]
        [InlineData("on-29")]
        [InlineData("on-30")]
        [InlineData("on-31")]
        [InlineData("on-32")]
        [InlineData("on-33")]
        [InlineData("on-34")]
        [InlineData("on-35")]
        [InlineData("on-36")]
        [InlineData("on-37")]
        [InlineData("on-38")]
        [InlineData("on-39")]
        [InlineData("on-40")]
        [InlineData("on-41")]
        [InlineData("on-42")]
        [InlineData("on-43")]
        [InlineData("on-44")]
        [InlineData("on-45")]
        [InlineData("on-46")]
        [InlineData("on-47")]
        [InlineData("on-48")]
        [InlineData("on-49")]
        [InlineData("on-50")]
        [InlineData("on-51")]
        [InlineData("on-52")]
        [InlineData("on-53")]
        [InlineData("on-54")]
        [InlineData("on-55")]
        [InlineData("on-56")]
        [InlineData("on-57")]
        [InlineData("on-58")]
        [InlineData("on-59")]
        [InlineData("on-60")]
        [InlineData("on-61")]
        [InlineData("on-62")]
        [InlineData("on-63")]
        [InlineData("on-64")]
        [InlineData("on-65")]
        [InlineData("on-66")]
        [InlineData("on-67")]
        [InlineData("on-68")]
        [InlineData("on-69")]
        [InlineData("on-70")]
        [InlineData("on-71")]
        [InlineData("on-72")]
        [InlineData("on-73")]
        [InlineData("on-74")]
        [InlineData("on-75")]
        [InlineData("on-76")]
        [InlineData("on-77")]
        [InlineData("on-78")]
        [InlineData("on-79")]
        [InlineData("on-80")]
        [InlineData("on-81")]
        [InlineData("on-82")]
        [InlineData("on-83")]
        [InlineData("on-84")]
        [InlineData("on-85")]
        [InlineData("on-86")]
        [InlineData("on-87")]
        [InlineData("on-88")]
        [InlineData("on-89")]
        [InlineData("on-90")]
        [InlineData("on-91")]
        [InlineData("on-92")]
        [InlineData("on-93")]
        [InlineData("on-94")]
        [InlineData("on-95")]
        [InlineData("on-96")]
        [InlineData("on-97")]
        [InlineData("on-98")]
        [InlineData("on-99")]
        [InlineData("on-100")]
        [InlineData("on-101")]
        [InlineData("on-102")]
        [InlineData("on-103")]
        [InlineData("on-104")]
        [InlineData("on-105")]
        [InlineData("on-106")]
        [InlineData("on-107")]
        [InlineData("on-108")]
        [InlineData("on-109")]
        [InlineData("on-110")]
        [InlineData("on-111")]
        [InlineData("on-112")]
        [InlineData("on-113")]
        [InlineData("on-114")]
        [InlineData("on-115")]
        [InlineData("on-116")]
        [InlineData("on-117")]
        [InlineData("on-118")]
        [InlineData("on-119")]
        [InlineData("on-120")]
        [InlineData("on-121")]
        [InlineData("on-122")]
        [InlineData("on-123")]
        [InlineData("on-124")]
        [InlineData("on-125")]
        [InlineData("on-126")]
        [InlineData("on-127")]
        [InlineData("on-128")]
        [InlineData("on-129")]
        [InlineData("on-130")]
        [InlineData("on-131")]
        [InlineData("on-132")]
        [InlineData("on-133")]
        [InlineData("on-134")]
        [InlineData("on-135")]
        [InlineData("on-136")]
        [InlineData("on-137")]
        [InlineData("on-138")]
        [InlineData("on-139")]
        [InlineData("on-140")]
        [InlineData("on-141")]
        [InlineData("on-142")]
        [InlineData("on-143")]
        [InlineData("on-144")]
        [InlineData("on-145")]
        [InlineData("on-146")]
        [InlineData("on-147")]
        [InlineData("on-148")]
        [InlineData("on-149")]
        [InlineData("on-150")]
        [InlineData("on-151")]
        [InlineData("on-152")]
        [InlineData("on-153")]
        [InlineData("on-154")]
        [InlineData("on-155")]
        [InlineData("on-156")]
        [InlineData("on-157")]
        [InlineData("on-158")]
        [InlineData("on-159")]
        [InlineData("on-160")]
        [InlineData("on-161")]
        [InlineData("on-162")]
        [InlineData("on-163")]
        [InlineData("on-164")]
        [InlineData("on-165")]
        [InlineData("on-166")]
        [InlineData("on-167")]
        [InlineData("on-168")]
        [InlineData("on-169")]
        [InlineData("on-170")]
        [InlineData("on-171")]
        [InlineData("on-172")]
        [InlineData("on-173")]
        [InlineData("on-174")]
        [InlineData("pe-1")]
        [InlineData("pe-2")]
        [InlineData("pe-3")]
        [InlineData("pe-4")]
        [InlineData("pe-5")]
        [InlineData("pe-6")]
        [InlineData("qc-2")]
        [InlineData("qc-5")]
        [InlineData("qc-11")]
        [InlineData("qc-13")]
        [InlineData("qc-14")]
        [InlineData("qc-15")]
        [InlineData("qc-16")]
        [InlineData("qc-17")]
        [InlineData("qc-18")]
        [InlineData("qc-19")]
        [InlineData("qc-20")]
        [InlineData("qc-21")]
        [InlineData("qc-22")]
        [InlineData("qc-24")]
        [InlineData("qc-25")]
        [InlineData("qc-26")]
        [InlineData("qc-27")]
        [InlineData("qc-28")]
        [InlineData("qc-29")]
        [InlineData("qc-30")]
        [InlineData("qc-33")]
        [InlineData("qc-35")]
        [InlineData("qc-36")]
        [InlineData("qc-38")]
        [InlineData("qc-40")]
        [InlineData("qc-41")]
        [InlineData("qc-42")]
        [InlineData("qc-45")]
        [InlineData("qc-46")]
        [InlineData("qc-47")]
        [InlineData("qc-48")]
        [InlineData("qc-49")]
        [InlineData("qc-50")]
        [InlineData("qc-51")]
        [InlineData("qc-52")]
        [InlineData("qc-53")]
        [InlineData("qc-54")]
        [InlineData("qc-55")]
        [InlineData("qc-56")]
        [InlineData("qc-57")]
        [InlineData("qc-58")]
        [InlineData("qc-59")]
        [InlineData("qc-60")]
        [InlineData("qc-61")]
        [InlineData("qc-62")]
        [InlineData("qc-63")]
        [InlineData("qc-64")]
        [InlineData("qc-65")]
        [InlineData("qc-66")]
        [InlineData("qc-67")]
        [InlineData("qc-68")]
        [InlineData("qc-69")]
        [InlineData("qc-70")]
        [InlineData("qc-71")]
        [InlineData("qc-72")]
        [InlineData("qc-73")]
        [InlineData("qc-74")]
        [InlineData("qc-76")]
        [InlineData("qc-77")]
        [InlineData("qc-78")]
        [InlineData("qc-81")]
        [InlineData("qc-83")]
        [InlineData("qc-84")]
        [InlineData("qc-85")]
        [InlineData("qc-89")]
        [InlineData("qc-90")]
        [InlineData("qc-92")]
        [InlineData("qc-93")]
        [InlineData("qc-94")]
        [InlineData("qc-95")]
        [InlineData("qc-96")]
        [InlineData("qc-97")]
        [InlineData("qc-98")]
        [InlineData("qc-99")]
        [InlineData("qc-100")]
        [InlineData("qc-101")]
        [InlineData("qc-102")]
        [InlineData("qc-103")]
        [InlineData("qc-104")]
        [InlineData("qc-105")]
        [InlineData("qc-106")]
        [InlineData("qc-107")]
        [InlineData("qc-108")]
        [InlineData("qc-109")]
        [InlineData("qc-110")]
        [InlineData("qc-111")]
        [InlineData("qc-112")]
        [InlineData("qc-113")]
        [InlineData("qc-114")]
        [InlineData("qc-115")]
        [InlineData("qc-116")]
        [InlineData("qc-117")]
        [InlineData("qc-118")]
        [InlineData("qc-119")]
        [InlineData("qc-120")]
        [InlineData("qc-121")]
        [InlineData("qc-122")]
        [InlineData("qc-123")]
        [InlineData("qc-124")]
        [InlineData("qc-125")]
        [InlineData("qc-126")]
        [InlineData("qc-127")]
        [InlineData("qc-128")]
        [InlineData("qc-129")]
        [InlineData("qc-130")]
        [InlineData("qc-131")]
        [InlineData("qc-132")]
        [InlineData("qc-133")]
        [InlineData("qc-134")]
        [InlineData("qc-135")]
        [InlineData("qc-136")]
        [InlineData("qc-137")]
        [InlineData("qc-138")]
        [InlineData("qc-139")]
        [InlineData("qc-140")]
        [InlineData("qc-141")]
        [InlineData("qc-142")]
        [InlineData("qc-143")]
        [InlineData("qc-144")]
        [InlineData("qc-145")]
        [InlineData("qc-146")]
        [InlineData("qc-147")]
        [InlineData("qc-148")]
        [InlineData("qc-149")]
        [InlineData("qc-150")]
        [InlineData("qc-151")]
        [InlineData("qc-152")]
        [InlineData("qc-153")]
        [InlineData("qc-154")]
        [InlineData("qc-155")]
        [InlineData("qc-156")]
        [InlineData("qc-157")]
        [InlineData("qc-158")]
        [InlineData("qc-159")]
        [InlineData("qc-160")]
        [InlineData("qc-161")]
        [InlineData("qc-162")]
        [InlineData("qc-163")]
        [InlineData("qc-165")]
        [InlineData("qc-166")]
        [InlineData("qc-167")]
        [InlineData("qc-168")]
        [InlineData("qc-169")]
        [InlineData("sk-1")]
        [InlineData("sk-2")]
        [InlineData("sk-3")]
        [InlineData("sk-4")]
        [InlineData("sk-5")]
        [InlineData("sk-6")]
        [InlineData("sk-7")]
        [InlineData("sk-8")]
        [InlineData("sk-9")]
        [InlineData("sk-10")]
        [InlineData("sk-11")]
        [InlineData("sk-12")]
        [InlineData("sk-13")]
        [InlineData("sk-14")]
        [InlineData("sk-15")]
        [InlineData("sk-16")]
        [InlineData("sk-17")]
        [InlineData("sk-18")]
        [InlineData("sk-19")]
        [InlineData("sk-20")]
        [InlineData("sk-21")]
        [InlineData("sk-22")]
        [InlineData("sk-23")]
        [InlineData("sk-24")]
        [InlineData("sk-25")]
        [InlineData("sk-26")]
        [InlineData("sk-27")]
        [InlineData("sk-28")]
        [InlineData("sk-29")]
        [InlineData("sk-30")]
        [InlineData("sk-31")]
        [InlineData("sk-32")]
        [InlineData("sk-33")]
        [InlineData("sk-34")]
        [InlineData("sk-35")]
        [InlineData("sk-36")]
        [InlineData("sk-37")]
        [InlineData("sk-38")]
        [InlineData("sk-39")]
        [InlineData("sk-40")]
        [InlineData("sk-41")]
        [InlineData("sk-42")]
        [InlineData("sk-43")]
        [InlineData("sk-44")]
        [InlineData("sk-45")]
        [InlineData("sk-46")]
        [InlineData("sk-47")]
        [InlineData("sk-48")]
        [InlineData("sk-49")]
        [InlineData("sk-50")]
        [InlineData("sk-51")]
        [InlineData("sk-52")]
        [InlineData("sk-53")]
        [InlineData("sk-54")]
        [InlineData("sk-55")]
        [InlineData("sk-56")]
        [InlineData("yt-1")]
        [InlineData("yt-2")]
        [InlineData("yt-3")]
        [InlineData("yt-4")]
        [InlineData("yt-5")]
        [InlineData("yt-6")]
        [InlineData("yt-7")]
        [InlineData("yt-8")]
        [InlineData("yt-9")]
        [InlineData("yt-10")]
        [InlineData("yt-11")]
        [InlineData("yt-12")]
        [InlineData("yt-13")]
        [InlineData("yt-14")]
        [InlineData("yt-15")]
        [InlineData("yt-16")]
        [InlineData("yt-17")]
        public void validateLocationCode_ShouldParseValidLocationCodes(string input)
        {
            Assert.True(EnvCanadaLocationValidator.validateLocationCode(input));
        }

    }
}
