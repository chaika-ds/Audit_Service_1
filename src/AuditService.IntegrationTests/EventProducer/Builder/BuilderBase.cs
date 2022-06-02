using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AuditService.IntegrationTests.EventProducer.Builder
{
    public abstract class BuilderBase<T> : IBuilderDto<T>
    {
        protected static readonly Random _random = new Random();
        protected static readonly string[] _provider = new string[] { "provaider 1", "provaider 2", "provaider 3", };
        protected static readonly string[] _catrgoryCodes = new string[] { "categorycode1", "categorycode3", "categorycode4", "categorycode2" };
        protected static readonly string[] _requestUrls = new string[] { "PUT: contracts/contractId?param=value", "POST: contracts/contractId?param=value"
            , "GET: contracts/contractId?param=value", "DELETE: contracts/contractId?param=value" };
        protected static readonly string[] _requestBodyCodes = new string[] { "request 1", "request 3", "request 2", "request 4" };
        protected static readonly string[] _entityNames = new string[] { "Entity 1", "Entity 4", "Entity 5", "Entity 2" };
        protected static readonly string[] _userIpCodes = new string[] { "192.168.123.132", "111.111.000.000", "000.000.000.000", "222.222.222.222" };
        protected static readonly string[] _userLogins = new string[] { "login1", "login2", "login3", "login4" };
        protected static readonly string[] _userAgents = new string[] { "Chrome", "Mozilla", "Safari", "Edge" };

        public virtual T Get()
        {
            var result = Activator.CreateInstance<T>();
            var returnedType = typeof(T);
            var methods = this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            var cc = returnedType.GetProperties();
            foreach (var field in returnedType.GetProperties())
            {
                if (field.PropertyType.IsEnum)
                {
                    field.SetValue(result, GetEnum(field.PropertyType));
                }
                else if (IsNullableEnum(field.PropertyType))
                {
                    field.SetValue(result, GetNullableEnum(field.PropertyType));
                }
                else if (field.PropertyType == typeof(Guid))
                {
                    field.SetValue(result, Guid.NewGuid());
                }
                else
                {
                    string key = $"Get{field.Name}";
                    var method = methods.FirstOrDefault(x => x.Name.Equals(key));
                    if (method != null)
                    {
                        var value = method.Invoke(this, null);
                        field.SetValue(result, value);
                    }
                }
            }
            return result;
        }
        protected virtual T GetEnum<T>()
        {
            return (T)GetEnum(typeof(T));
        }

        private object GetEnum(Type type)
        {
            Array values = Enum.GetValues(type);
            return values.GetValue(_random.Next(values.Length));
        }

        private object GetNullableEnum(Type type)
        {
            Array values = Enum.GetValues(Nullable.GetUnderlyingType(type));

            var rndMax = values.Length;
            var rnd = _random.Next(rndMax + 1);

            if (rnd == rndMax)
            {
                return null;
            }

            return values.GetValue(rnd);
        }

        protected virtual string GetCategoryCode()
        {
            return _catrgoryCodes[_random.Next(0, _catrgoryCodes.Length - 1)];
        }

        protected virtual string GetRequestUrl()
        {
            return _requestUrls[_random.Next(0, _requestUrls.Length - 1)];
        }

        protected virtual string GetRequestBody()
        {
            return _requestBodyCodes[_random.Next(0, _requestBodyCodes.Length - 1)];
        }

        protected virtual DateTime GetTimeStamp()
        {
            var rnd = _random.Next(-2, 1);
            var rndD = _random.NextDouble();
            return DateTime.UtcNow.AddHours(rndD + rnd);
        }

        protected virtual string GetEntityName()
        {
            return _entityNames[_random.Next(0, _entityNames.Length - 1)];
        }

        protected string GetJson(string jsonAdress)
        {
            using (StreamReader r = new StreamReader(jsonAdress))
            {
                var json = r.ReadToEnd();
                return json;
            }
        }
        protected virtual string GetIp()
        {
            return _userIpCodes[_random.Next(0, _userLogins.Length - 1)];
        }

        protected virtual string GetLogin()
        {
            return _userLogins[_random.Next(0, _userLogins.Length - 1)];
        }

        protected virtual string GetUserAgent()
        {
            return _userAgents[_random.Next(0, _userAgents.Length - 1)];
        }

        private bool IsNullableEnum(Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }
    }
}
