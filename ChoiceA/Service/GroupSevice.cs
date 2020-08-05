using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Service
{
    public interface IGroupService
    {
        public List<string> GetGroupList();
    }
    public class GroupSevice:IGroupService
    {
        public List<string> GetGroupList()
        {  
            return GroupList.getInstance().Groups;
        }
    }
    public static class GrupServiceExtention
    {
        public static IServiceCollection GetGroupList(this IServiceCollection services)
            => services.AddTransient<IGroupService, GroupSevice>();
    }
}
