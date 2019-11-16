using NetCoreMvcVueElementUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMvcVueElementUI.Service
{
    public class MenuService
    {
        public List<Menu> GetLetfMenus()
        {
            List<Menu> menus = new List<Menu>()
            {
                    new Menu("导航一","path","name1", "el-icon-menu","1","0" ),
                        new Menu("分组一","path","name2","el-icon-location","2","1"),
                            new Menu("选项一","/path","/home/page1","el-icon-location","3","2"),
                            new Menu("选项二","/path","/home/page2","el-icon-location","4","2"),
                        new Menu("分组二","/path","name5","el-icon-location","5","1"),
                        new Menu("分组三","path","name6","el-icon-location","6","1"),
                            new Menu("选项一","/path","name7","el-icon-location","7","6"),
                            new Menu("选项二","/path","name8","el-icon-location","8","6"),

                    new Menu("导航二","path","name9", "el-icon-menu","9","0"),
                        new Menu("分组一","path","name10","el-icon-location","10","9"),
                            new Menu("选项一","path","name11","el-icon-location","11","10"),
                            new Menu("选项二","path","name12","el-icon-location","12","10"),
                        new Menu("分组二","/path","name13","el-icon-location","13","9"),

                    new Menu("导航三","/path","name", "el-icon-menu","14","0")
            };
            return menus;
        }      
      
    }
}
