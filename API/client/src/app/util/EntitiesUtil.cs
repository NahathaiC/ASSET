using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.client.src.app.util
{
    // src/Controllers/EntitiesUtil.cs

    public class EntitiesUtil
    {
        public static List<string> EnsureEntitiesArray(List<User> entities)
        {
            if (entities == null)
            {
                return new List<string>();
            }

            return entities.Select(entity => entity.Id.ToString()).ToList();
        }
    }

}