import Home from "../views/Home.vue";
import Login from "../views/Login.vue";
import User from "../views/user/index.vue";
import Layout from "../views/Layout.vue";
import EmptyLayout from "../views/EmptyLayout.vue";
import About from "../views/About.vue";
import Consulting from "../views/consulting/index.vue";
import Company from "../views/system/company/index.vue";
import Role from "../views/system/role/index.vue";
import Apartment from "../views/system/apartment/index.vue";
import PricePackage from "../views/system/apartment/pricePackage/index.vue";
import consumable from "../views/system/consumable/index.vue";
import RoomType from "../views/system/roomType/index.vue";
import AppreciationService from "../views/system/appreciationService/index.vue";
import SalesChart from "../views/sales/chart/room.vue";
import Customer from "../views/customer/index.vue";
import Setting from "../views/system/setting/index.vue";
import Page403 from "../views/exception/403.vue";
import Page404 from "../views/exception/404.vue";
import Page500 from "../views/exception/500.vue";
import WorkFlow from "../views/system/workflow/index.vue";

const routes = [
    {
        name: "login",
        path: "/login",
        component: Login,
        meta: {
            title: "登录",
            icon: "home",
            hidden: true
        }
    },
    {
        path: "/",
        redirect: "/home",
        meta: {
            role: "all",
            hidden: true
        }
    },
    {
        path: "/home",
        component: EmptyLayout,
        meta: {
            role: "all",
            title: "首页",
            icon: "home"
        },
        children: [
            {
                path: "",
                name: "home",
                component: Home,
                meta: {
                    title: "首页"
                }
            },
            {
                path: "/contract/approve",
                name: "contract-approve",
                component: () => import("../views/approve/contract.vue"),
                meta: {
                    title: "合同审批",
                    category: "咨询管理",
                    role: "合同管理",
                    subRole: "审批",
                    hidden: true
                }
            },
            {
                path: "/room-change/approve",
                name: "room-change-approve",
                component: () => import("../views/approve/room-change.vue"),
                meta: {
                    title: "换房审批",
                    category: "咨询管理",
                    role: "合同管理",
                    subRole: "审批",
                    hidden: true
                }
            },
            {
                path: "/service-pack-change/approve",
                name: "service-pack-change-approve",
                component: () => import("../views/approve/service-pack-change.vue"),
                meta: {
                    title: "换服务包审批",
                    category: "咨询管理",
                    role: "合同管理",
                    subRole: "审批",
                    hidden: true
                }
            }
        ]
    },
    {
        path: "/sales",

        component: Layout,
        meta: {
            title: "销售管理",
            role: "销售管理"
        },
        children: [
            {
                path: "",
                component: Consulting,
                name: "sales",
                meta: {
                    title: "咨询接待",
                    category: "咨询管理",
                    role: "咨询接待",
                    subRole: "查看"
                }
            },
            {
                path: "/contract/create/:id",
                name: "contract-create",
                component: () => import("../views/contract/create.vue"),
                meta: {
                    title: "合同创建",
                    category: "咨询管理",
                    role: "合同管理",
                    subRole: "新增",
                    hidden: true
                }
            },
            {
                path: "/contract/edit/:id",
                name: "contract-edit",
                component: () => import("../views/contract/edit.vue"),
                meta: {
                    title: "合同编辑",
                    category: "咨询管理",
                    role: "合同管理",
                    subRole: "编辑",
                    hidden: true
                }
            },
            {
                path: "/contract/details/:id",
                name: "contract-details",
                component: () => import("../views/contract/details.vue"),
                meta: {
                    title: "合同详情",
                    category: "咨询管理",
                    role: "合同管理",
                    subRole: "查看",
                    hidden: true
                }
            },
            {
                path: "/consulting/favorite",
                name: "consulting-favorite",
                component: () => import("../views/consulting/favorite.vue"),
                meta: {
                    title: "我的收藏",
                    category: "咨询管理",
                    role: "咨询接待",
                    subRole: "查看"
                }
            },
            {
                path: "/consulting/trace",
                component: () => import("../views/consulting/trace.vue"),
                meta: {
                    title: "跟踪列表",
                    category: "咨询管理",
                    role: "跟踪列表",
                    subRole: "查看"
                }
            },
            {
                path: "/consulting/create",
                name: "consulting-create",
                component: () => import("../views/consulting/create.vue"),
                meta: {
                    title: "咨询接待新增",
                    category: "咨询管理",
                    role: "咨询接待",
                    subRole: "新增",
                    hidden: true
                }
            },
            {
                path: "/consulting/edit/:id",
                name: "consulting-edit",
                component: () => import("../views/consulting/edit.vue"),
                meta: {
                    title: "咨询接待编辑",
                    category: "咨询管理",
                    role: "咨询接待",
                    subRole: "编辑",
                    hidden: true
                }
            },
            {
                path: "/consulting/details/:id",
                name: "consulting-details",
                component: () => import("../views/consulting/details.vue"),
                meta: {
                    title: "咨询接待详情",
                    category: "咨询管理",
                    role: "咨询接待",
                    subRole: "查看",
                    hidden: true
                }
            },
            {
                path: "/sales/chart/room",
                component: SalesChart,
                meta: {
                    title: "销控图",
                    category: "销控图",
                    role: "销控图",
                    subRole: "查看"
                }
            }
        ]
    },
    {
        path: "/customer",

        component: Layout,
        meta: {
            title: "客户管理",
            role: "客户管理"
        },
        children: [
            {
                path: "",
                component: Customer,
                name: "customer",
                meta: {
                    title: "客户信息",
                    icon: "el-icon-service",
                    category: "客户管理",
                    role: "客户信息",
                    subRole: "查看"
                }
            },
            {
                path: "/customer/waiting/:id",
                name: "customer-waiting",
                component: () => import("../views/customer/waiting.vue"),
                meta: {
                    title: "合同查看",
                    category: "客户信息",
                    role: "客户信息",
                    subRole: "查看",
                    hidden: true
                }
            },
            {
                path: "/customer/details/:id",
                name: "customer-details",
                component: () => import("../views/customer/details.vue"),
                meta: {
                    title: "客户详情",
                    category: "客户信息",
                    role: "客户信息",
                    subRole: "查看",
                    hidden: true
                }
            }
        ]
    },
    {
        path: "/finance",
        name: "finance",
        component: Layout,
        meta: {
            title: "财务管理",
            role: "财务管理"
        },
        children: [
            {
                path: "/finance/cash-account",
                name: "cashAccount",
                component: () => import("../views/finance/cashAccount.vue"),
                meta: {
                    title: "现金账户",
                    category: "客户账户管理",
                    role: "现金账户",
                    subRole: "查看"
                }
            },
            {
                path: "/finance/cash-flow",
                name: "cashFlow",
                component: () => import("../views/finance/cashFlow.vue"),
                meta: {
                    title: "现金账户流水",
                    category: "客户账户管理",
                    role: "现金账户流水",
                    subRole: "查看"
                }
            },
            {
                path: "/finance/point-account",
                name: "pointAccount",
                component: () => import("../views/finance/pointAccount.vue"),
                meta: {
                    title: "积分账户",
                    category: "客户账户管理",
                    role: "积分账户",
                    subRole: "查看"
                }
            },
            {
                path: "/finance/point-flow",
                name: "pointFlow",
                component: () => import("../views/finance/pointFlow.vue"),
                meta: {
                    title: "积分账户流水",
                    category: "客户账户管理",
                    role: "积分账户流水",
                    subRole: "查看"
                }
            },
            {
                path: "/finance/deposit-bill",
                component: Consulting,
                meta: {
                    title: "押金账单",
                    category: "账单管理",
                    role: "押金账单",
                    subRole: "查看"
                }
            },
            {
                path: "/finance/monthly-fee-bill",
                component: Consulting,
                meta: {
                    title: "月费账单",
                    category: "账单管理",
                    role: "月费账单",
                    subRole: "查看"
                }
            },
            {
                path: "/finance/settlement-fee-bill",
                component: Consulting,
                meta: {
                    title: "一次性安置费账单",
                    category: "账单管理",
                    role: "一次性安置费账单",
                    subRole: "查看"
                }
            }
        ]
    },
    {
        path: "/report",
        name: "report",
        component: Layout,
        meta: {
            title: "报表管理",
            role: "报表管理"
        },
        children: []
    },
    {
        path: "/system",
        name: "system",
        component: Layout,
        meta: {
            title: "系统管理",
            role: "系统管理"
        },
        children: [
            {
                path: "custom-form",
                component: () => import("$v/system/custom-form/index.vue"),
                meta: {
                    title: "自定义表单",
                    category: "配置项",
                    role: "自定义表单"
                }
            },
            {
                path: "/system/apartment",
                component: Apartment,
                meta: {
                    title: "公寓信息",
                    category: "公寓管理",
                    role: "公寓信息",
                    subRole: "查看"
                }
            },
            {
                path: "/system/apartment/create",
                name: "apartment-create",
                component: () => import("../views/system/apartment/create.vue"),
                meta: {
                    title: "新增公寓",
                    category: "公寓管理",
                    role: "公寓信息",
                    subRole: "新增",
                    hidden: true
                }
            },
            {
                path: "/system/apartment/edit/:id/:disabled",
                name: "apartment-edit",
                component: () => import("../views/system/apartment/create.vue"),
                meta: {
                    title: "编辑公寓",
                    category: "公寓管理",
                    role: "公寓信息",
                    subRole: "编辑",
                    hidden: true
                }
            },
            {
                path: "/system/apartment/details/:id/:disabled",
                name: "apartment-details",
                component: () => import("../views/system/apartment/create.vue"),
                meta: {
                    title: "查看公寓",
                    category: "公寓管理",
                    role: "公寓信息",
                    subRole: "查看",
                    hidden: true
                }
            },
            {
                path: "/system/apartment/room/:id",
                name: "apartment-room",
                component: () => import("../views/system/apartment/room.vue"),
                meta: {
                    title: "房间信息",
                    category: "公寓管理",
                    role: "公寓信息",
                    subRole: "查看",
                    hidden: true
                }
            },

            {
                path: "/system/room-type",
                name: "room-type",
                component: RoomType,
                meta: {
                    title: "房型信息",
                    category: "公寓管理",
                    role: "房型信息",
                    subRole: "查看"
                }
            },
            {
                path: "/system/appreciation-service",
                component: AppreciationService,
                meta: {
                    title: "服务项目",
                    category: "费用管理",
                    role: "服务项目",
                    subRole: "查看"
                }
            },
            {
                path: "/system/consumable",
                component: consumable,
                meta: {
                    title: "服务耗材",
                    category: "费用管理",
                    role: "服务耗材",
                    subRole: "查看"
                }
            },
            {
                path: "/system/apartment/pricePackage",
                component: PricePackage,
                meta: {
                    title: "费用包",
                    category: "费用管理",
                    role: "费用包",
                    subRole: "查看"
                }
            },
            {
                path: "/system/apartment/pricePackage/create",
                name: "price-package-create",
                component: () => import("../views/system/apartment/pricePackage/create.vue"),
                meta: {
                    title: "创建费用包",
                    category: "费用管理",
                    role: "费用包",
                    subRole: "创建",
                    hidden: true
                }
            },
            {
                path: "/system/apartment/pricePackage/edit/:id",
                name: "price-package-edit",
                component: () => import("../views/system/apartment/pricePackage/create.vue"),
                meta: {
                    title: "编辑费用包",
                    category: "费用管理",
                    role: "费用包",
                    subRole: "编辑",
                    hidden: true
                }
            },
            {
                path: "/system/apartment/pricePackage/details/:id",
                name: "price-package-details",
                component: () => import("../views/system/apartment/pricePackage/create.vue"),
                meta: {
                    title: "费用包详情",
                    category: "费用管理",
                    role: "费用包",
                    subRole: "查看",
                    hidden: true
                }
            },
            {
                path: "/system/role",
                component: Role,
                meta: {
                    title: "角色信息",
                    category: "员工管理",
                    role: "角色信息",
                    subRole: "查看"
                }
            },
            {
                path: "/system/user",
                component: User,
                meta: {
                    title: "员工信息",
                    category: "员工管理",
                    role: "员工管理",
                    subRole: "查看"
                }
            },
            {
                path: "/system/setting",
                component: Setting,
                name: "system-setting",
                meta: {
                    title: "设置",
                    category: "配置项",
                    role: "设置",
                    subRole: "查看"
                }
            },
            {
                path: "/system/workflow",
                component: WorkFlow,
                meta: {
                    title: "工作流配置",
                    category: "工作流配置",
                    role: "工作流配置",
                    subRole: "查看"
                }
            },
            {
                path: "/system/workflow/create",
                name: "workflow-create",
                component: () => import("../views/system/workflow/create.vue"),
                meta: {
                    title: "新建工作流",
                    category: "工作流配置",
                    role: "工作流配置",
                    subRole: "新建",
                    hidden: true
                }
            },
            {
                path: "/system/workflow/edit/:id",
                name: "workflow-edit",
                component: () => import("../views/system/workflow/edit.vue"),
                meta: {
                    title: "编辑工作流",
                    category: "工作流配置",
                    role: "工作流配置",
                    subRole: "编辑",
                    hidden: true
                }
            },
            {
                path: "/system/workflow/view/:id",
                name: "workflow-view",
                component: () => import("../views/system/workflow/view.vue"),
                meta: {
                    title: "查看工作流",
                    category: "工作流配置",
                    role: "工作流配置",
                    subRole: "查看",
                    hidden: true
                }
            }
        ]
    },
    {
        name: "403",
        path: "/exception/403",
        component: Page403,
        meta: {
            title: "403",
            role: "all",
            hidden: true
        }
    },
    {
        name: "404",
        path: "/exception/404",
        component: Page404,
        meta: {
            title: "404",
            role: "all",
            hidden: true
        }
    },
    {
        name: "500",
        path: "/exception/500",
        component: Page500,
        meta: {
            title: "500",
            role: "all",
            hidden: true
        }
    }
];
export const accesses = {
    1: "查看",
    2: "新增",
    4: "编辑",
    8: "删除",
    16: "重置",
    32: "导入",
    64: "导出",
    128: "管理他人",
    256: "审批"
};

export function convert(bitmask) {
    var arr = [];
    for (let i = 0; i < 32; i++) {
        let mask = 1 << i;
        if ((bitmask & mask) != 0) {
            arr.push(mask);
        }
    }
    return arr;
}

// export function filterRoutes(loginUser) {
//     console.log(loginUser);
//     if (loginUser.superAdmin) {
//         let arr = _.cloneDeep(routes);
//         return arr;
//     }
//     if (loginUser.companyAdmin) {
//         let arr = _.cloneDeep(routes);
//         let systemSetting = _.find(arr, x => x.meta && x.meta.role == "系统管理");
//         systemSetting.children = systemSetting.children.filter(x => x.meta.role != "机构信息");

//         return arr;
//     }
//     if (loginUser.userPermissions) {
//         let arr = [];
//         routes.forEach(r => {
//             if (r.meta.role == "all") {
//                 arr.push(_.cloneDeep(r));
//             }
//             loginUser.userPermissions.forEach(u => {
//                 if (r.meta.role == u.name) {
//                     let business = _.cloneDeep(r);
//                     if (business.children) {
//                         let categoryArr = [];
//                         business.children.forEach(c => {
//                             u.children.forEach(uc => {
//                                 if (uc.children) {
//                                     uc.children.forEach(m => {
//                                         if (c.meta.role == "all") {
//                                             let category = _.cloneDeep(c);
//                                             category.meta.permission = m.permission;
//                                             categoryArr.push(category);
//                                         }
//                                         if (c.meta.role == m.name) {
//                                             if (m.name == "跟踪列表") {
//                                                 console.log("跟踪列表:" + m.permission);
//                                             }
//                                             let permissions = convert(m.permission).map(x => accesses[x]);
//                                             if (_.includes(permissions, c.meta.subRole)) {
//                                                 let category = _.cloneDeep(c);
//                                                 category.meta.permission = m.permission;
//                                                 categoryArr.push(category);
//                                             }

//                                             // console.log(c.meta.role+" "+c.meta.subRole);
//                                             // console.log(m.name + " "+permissions.map(x=>accesses[x]).join(","));
//                                         }
//                                     });
//                                 }
//                             });
//                         });
//                         business.meta.permission = categoryArr[0].meta.permission;
//                         business.children = categoryArr;
//                     }
//                     arr.push(business);
//                 }
//             });
//         });
//         return arr;
//     }
// }

export default routes;
