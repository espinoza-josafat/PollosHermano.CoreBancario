using PollosHermano.CoreBancario.Domian.SysCore.Dao;
using PollosHermano.CoreBancario.Domian.SysCore.Processes;
using PollosHermano.CoreBancario.Entities.SysCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Application.SysCore.Processes
{
    public class ProcessGetMenusByUser : IProcessGetMenusByUser
    {
        readonly IMenuDao _menuDao;

        public ProcessGetMenusByUser(IMenuDao menuDao)
        {
            _menuDao = menuDao;
        }

        public async Task<List<MenuModel>> ExecuteAsync(Guid userId)
        {
            var result = new List<MenuModel>();

            var menus = await _menuDao.GetMenusByUser(userId);
            if (menus != null && menus.Any())
            {
                var parents = menus.Where(x => !x.MenuIdParent.HasValue).OrderBy(x=> x.Order);
                if (parents != null && parents.Any())
                {
                    result.AddRange(parents);

                    foreach (var parent in result)
                    {
                        AddChildrensRecursively(parent, menus);
                    }
                }
            }

            return result;
        }

        void AddChildrensRecursively(MenuModel model, IEnumerable<MenuModel> menus)
        {
            var items = menus.Where(x => x.MenuIdParent.HasValue && x.MenuIdParent.Value == model.Id).OrderBy(x => x.Order);
            if (items != null && items.Any())
            {
                model.Childrens.AddRange(items);

                foreach (var children in model.Childrens)
                {
                    AddChildrensRecursively(children, menus);
                }
            }
        }
    }
}
