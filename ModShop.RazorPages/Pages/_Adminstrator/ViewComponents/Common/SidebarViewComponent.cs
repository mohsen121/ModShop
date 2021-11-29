using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents.Common
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new SidebarViewModel
            {
                Logo = null,
                Menus = new List<GroupMenu>
                {
                    new GroupMenu
                    {
                        Icon = "flaticon-dashboard",
                        Title = "داشبورد",
                        Url = "/Adminstrator"
                    },
                    new GroupMenu
                    {
                        Title = "محتوا",
                        Items = new List<GroupMenuItem>
                        {
                            new GroupMenuItem
                            {
                                Title = "محصولات",
                                IsParent = true,
                                IsRoot = false,
                                Icon = "flaticon-cart",
                                Url = "/Adminstrator/Products",
                                Children = new List<GroupMenuItem>
                                {
                                    new GroupMenuItem
                                    {
                                        Title = "لیست",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Products"
                                    },
                                    new GroupMenuItem
                                    {
                                        Title = "محصول جدید",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon2-pen",
                                        Url = "/Adminstrator/Products/New"
                                    }
                                }
                            },

                            new GroupMenuItem
                            {
                                Title = "دسته بندی ها",
                                IsParent = true,
                                IsRoot = false,
                                Icon = "flaticon-cogwheel-1",
                                Url = "/Adminstrator/Categories",
                                Children = new List<GroupMenuItem>
                                {
                                    new GroupMenuItem
                                    {
                                        Title = "لیست",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Categories"
                                    },
                                    new GroupMenuItem
                                    {
                                        Title = "دسته بندی جدید",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon2-pen",
                                        Url = "/Adminstrator/Categories/New"
                                    }
                                }
                            },

                            new GroupMenuItem
                            {
                                Title = "برند ها",
                                IsParent = true,
                                IsRoot = false,
                                Icon = "flaticon-medal",
                                Url = "/Adminstrator/Brands",
                                Children = new List<GroupMenuItem>
                                {
                                    new GroupMenuItem
                                    {
                                        Title = "لیست",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Brands"
                                    },
                                    new GroupMenuItem
                                    {
                                        Title = "دسته بندی جدید",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon2-pen",
                                        Url = "/Adminstrator/Brands/New"
                                    }
                                }
                            },

                            new GroupMenuItem
                            {
                                Title = "رنگ ها",
                                IsParent = true,
                                IsRoot = false,
                                Icon = "flaticon-layer",
                                Url = "/Adminstrator/Colors",
                                Children = new List<GroupMenuItem>
                                {
                                    new GroupMenuItem
                                    {
                                        Title = "لیست",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Colors"
                                    },
                                    new GroupMenuItem
                                    {
                                        Title = "رنگ جدید",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon2-pen",
                                        Url = "/Adminstrator/Colors/New"
                                    }
                                }
                            },

                            new GroupMenuItem
                            {
                                Title = "سایز ها",
                                IsParent = true,
                                IsRoot = false,
                                Icon = "flaticon-tool",
                                Url = "/Adminstrator/Sizes",
                                Children = new List<GroupMenuItem>
                                {
                                    new GroupMenuItem
                                    {
                                        Title = "لیست",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Sizes"
                                    },
                                    new GroupMenuItem
                                    {
                                        Title = "سایز جدید",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon2-pen",
                                        Url = "/Adminstrator/Sizes/New"
                                    }
                                }
                            },

                            new GroupMenuItem
                            {
                                Title = "اسلایدر ها",
                                IsParent = true,
                                IsRoot = false,
                                Icon = "flaticon-tool",
                                Url = "/Adminstrator/Sliders",
                                Children = new List<GroupMenuItem>
                                {
                                    new GroupMenuItem
                                    {
                                        Title = "لیست",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Sliders"
                                    },
                                    new GroupMenuItem
                                    {
                                        Title = "اسلایدر جدید",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon2-pen",
                                        Url = "/Adminstrator/Sliders/New"
                                    }
                                }
                            },


                        }
                    },
                    new GroupMenu
                    {
                        Title = "کاربران",
                        Items = new List<GroupMenuItem>
                        {
                            new GroupMenuItem
                            {
                                Title = "لیست کاربران",
                                IsRoot = true,
                                IsParent = false,
                                Icon = "flaticon-list",
                                Url = "/Adminstrator/Users"
                            },
                            new GroupMenuItem
                            {
                                Title = "مدیران",
                                IsParent = true,
                                IsRoot = false,
                                Icon = "flaticon-user",
                                Url = "/Adminstrator/Admins",
                                Children = new List<GroupMenuItem>
                                {
                                    new GroupMenuItem
                                    {
                                        Title = "لیست مدیران",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Admins"
                                    },
                                    new GroupMenuItem
                                    {
                                        Title = "مدیر جدید",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon2-pen",
                                        Url = "/Adminstrator/Admins/New"
                                    },

                                }
                            }
                        }
                    },

                    new GroupMenu
                    {
                        Title = "سفارش ها",
                        Items = new List<GroupMenuItem>
                        {
                            new GroupMenuItem
                            {
                                Title = "لیست پرداخت ها",
                                IsRoot = true,
                                IsParent = false,
                                Icon = "flaticon-list",
                                Url = "/Adminstrator/Payments"
                            },
                            new GroupMenuItem
                            {
                                Title = "لیست سفارش ها",
                                IsRoot = true,
                                IsParent = false,
                                Icon = "flaticon-list",
                                Url = "/Adminstrator/Orders"
                            },

                            new GroupMenuItem
                            {
                                Title = "لیست ریز سفارش ها",
                                IsRoot = true,
                                IsParent = false,
                                Icon = "flaticon-list",
                                Url = "/Adminstrator/OrderItems"
                            },

                            new GroupMenuItem
                            {
                                Title = "گزارش های مالی",
                                IsParent = true,
                                IsRoot = false,
                                Icon = "flaticon-visible",
                                Url = "/Adminstrator/Reports",
                                Children = new List<GroupMenuItem>
                                {
                                    new GroupMenuItem
                                    {
                                        Title = "گزارش فروش به تفکیک محصول",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Reports/SaleProductSeprated"
                                    },
                                    new GroupMenuItem
                                    {
                                        Title = "گزارش فروش ناخالص هفتگی",
                                        IsRoot = true,
                                        IsParent = false,
                                        Icon = "flaticon-list",
                                        Url = "/Adminstrator/Reports/Sale"
                                    },

                                }
                            }
                        }
                    },


                }
            };
            return await Task.FromResult((IViewComponentResult)View("~/Pages/_Adminstrator/ViewComponents/Common/_Sidebar.cshtml", vm));
        }
    }
}
