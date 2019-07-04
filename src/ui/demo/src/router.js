import Vue from "vue";
import Router from "vue-router";
import Login from "./views/Login.vue";
import routes, { accesses, convert } from "./config/fullRoutes";
const Categories = [
    {
        name: "员工管理",
        icon: "far el-icon-menu"
    },
    {
        name: "咨询管理",
        icon: "far el-icon-menu"
    },
    {
        name: "公寓管理",
        icon: "far fa-building"
    },
    {
        name: "自定义表单",
        icon: "far fa-building"
    },
    {
        name: "费用管理",
        icon: "far el-icon-document"
    },
    {
        name: "客户账户管理",
        icon: "far el-icon-document"
    },
    {
        name: "账单管理",
        icon: "far el-icon-tickets"
    },
    {
        name: "销控图",
        icon: "far el-icon-picture-outline"
    },
    {
        name: "客户管理",
        icon: "far el-icon-menu"
    },
    {
        name: "配置项",
        icon: "far el-icon-setting"
    },
    {
        name: "工作流配置",
        icon: "far el-icon-tickets"
    }
];

Vue.use(Router);

// 页面刷新时，重新赋值token

const router = new Router({
    mode: "history",
    routes: routes
});

export default router;
export { Categories };
