using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Responses;
using Xunit.Sdk;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public static class ShouldExtensions
    {
        /// <summary>
        /// A working soltion instead of non working ShouldBeEquivalentTo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void ShouldBeValueEqualTo<T>(this T x, T y)
        {
            AreValueEqual(typeof(T), x, y);
        }

        private static void AreValueEqual(Type type, object a, object b)
        {
            if (ReferenceEquals(a, b))
                return;

            if (a == null && b == null)
                return;

            if (a == null || b == null)
                a.Should().Be(b);

            if (IsEnumerableType(type))
            {
                var enum1 = a as IEnumerable;
                enum1.Should().NotBeNull();

                var enum2 = b as IEnumerable;
                enum2.Should().NotBeNull();

                var e1 = enum1.GetEnumerator();
                var e2 = enum2.GetEnumerator();

                while (e1.MoveNext() && e2.MoveNext())
                {
                    AreValueEqual(e1.Current.GetType(), e1.Current, e2.Current);
                }
                return;
            }

            if (type == typeof(object))
                throw new AssertException("You need to specify type to do the value equality comparision.");

            if (IsSimpleType(type))
            {
                a.Should().Be(b);
                return;
            }

            var properties = type.GetProperties();
            if (properties.Length == 0)
            {
                if (!Equals(a, b))
                    throw new AssertException(string.Format("Instances of type '{0}' are not equal.", type.Name));
            }

            foreach (var propertyInfo in type.GetProperties())
            {
                var propertyType = propertyInfo.PropertyType;
                var valueForA = propertyInfo.GetValue(a, null);
                var valueForB = propertyInfo.GetValue(b, null);

                var isSimpleType = IsSimpleType(propertyType);
                if (isSimpleType)
                    valueForA.Should().Be(valueForB, string.Format("Values in property '{0}' doesn't match.", propertyInfo.Name));
                else
                    AreValueEqual(propertyType, valueForA, valueForB);
            }
        }

        [DebuggerStepThrough]
        public static HttpRequestShouldBe ShouldBe(this HttpRequest request)
        {
            return new HttpRequestShouldBe(request);
        }

        [DebuggerStepThrough]
        public static CreateOrderResponseShouldBe ShouldBe(this CreateOrderResponse response)
        {
            return new CreateOrderResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static AuthorizeInvoiceResponseShouldBe ShouldBe(this AuthorizeInvoiceResponse response)
        {
            return new AuthorizeInvoiceResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static InitializePaymentResponseShouldBe ShouldBe(this InitializePaymentResponse response)
        {
            return new InitializePaymentResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static FinalizeAuthorizationResponseShouldBe ShouldBe(this FinalizeAuthorizationResponse response)
        {
            return new FinalizeAuthorizationResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static RefundPaymentResponseShouldBe ShouldBe(this RefundPaymentResponse response)
        {
            return new RefundPaymentResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static AnnulAuthorizationResponseShouldBe ShouldBe(this AnnulAuthorizationResponse response)
        {
            return new AnnulAuthorizationResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static GetAddressesResponseShouldBe ShouldBe(this GetAddressesResponse response)
        {
            return new GetAddressesResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static GetCustomerProfileResponseShouldBe ShouldBe(this GetCustomerProfileResponse response)
        {
            return new GetCustomerProfileResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static GetPaymentOptionsResponseShouldBe ShouldBe(this GetPaymentOptionsResponse response)
        {
            return new GetPaymentOptionsResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static PaynovaSdkExceptionShouldBe ShouldBe(this PaynovaSdkException ex)
        {
            return new PaynovaSdkExceptionShouldBe(ex);
        }

        private static readonly HashSet<Type> ExtraPrimitiveTypes = new HashSet<Type> { typeof(string), typeof(Guid), typeof(DateTime), typeof(Decimal) };
        private static readonly HashSet<Type> ExtraPrimitiveNullableTypes = new HashSet<Type> { typeof(Guid?), typeof(DateTime?), typeof(Decimal?) };

        private static bool IsSimpleType(Type type)
        {
            return (type.IsGenericType == false && type.IsValueType) || type.IsPrimitive || type.IsEnum || ExtraPrimitiveTypes.Contains(type) || IsNullablePrimitiveType(type);
        }

        private static bool IsEnumerableType(Type type)
        {
            return type != typeof(string)
                && type.IsValueType == false
                && type.IsPrimitive == false
                && typeof(IEnumerable).IsAssignableFrom(type);
        }

        private static bool IsNullablePrimitiveType(Type t)
        {
            return ExtraPrimitiveNullableTypes.Contains(t) || (t.IsValueType && t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>) && t.GetGenericArguments()[0].IsPrimitive);
        }
    }
}