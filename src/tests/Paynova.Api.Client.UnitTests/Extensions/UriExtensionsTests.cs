﻿using System;
using FluentAssertions;
using Paynova.Api.Client.Extensions;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Extensions
{
    public class UriExtensionsTests : UnitTests
    {
        [Fact]
        public void Getting_absolute_uri_excluding_user_info_When_user_info_exists_It_should_remove_user_info()
        {
            var r = new Uri("https://s%40:p%40ssword@api.foo.com:8080/resource/1").GetAbsoluteUriExceptUserInfo();

            r.Should().Be("https://api.foo.com:8080/resource/1");
        }

        [Fact]
        public void Getting_absolute_uri_excluding_user_info_When_no_user_info_exists_It_should_remove_user_info()
        {
            var r = new Uri("https://api.foo.com:8080/resource/1").GetAbsoluteUriExceptUserInfo();

            r.Should().Be("https://api.foo.com:8080/resource/1");
        }

        [Fact]
        public void Getting_user_info_parts_When_encoded_user_and_password_are_provided_It_extracts_decoded_user_and_password()
        {
            var r = new Uri("https://s%40:p%40ssword@api.foo.com").GetUserInfoParts();

            r.Should().BeEquivalentTo("s@", "p@ssword");
        }

        [Fact]
        public void Getting_user_info_parts_When_non_encoded_user_and_password_are_provided_It_extracts_user_and_password()
        {
            var r = new Uri("https://tstUser:tstPwd@api.foo.com").GetUserInfoParts();

            r.Should().BeEquivalentTo("tstUser", "tstPwd");
        }

        [Fact]
        public void Getting_user_info_parts_When_nothing_is_provided_It_returns_empty_array()
        {
            var r = new Uri("https://api.foo.com").GetUserInfoParts();

            r.Should().NotBeNull();
            r.Should().BeEmpty();
        }
    }
}