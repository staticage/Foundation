<template>
    <el-container :style="{height: (fullHeight ) + 'px'}">
        <el-header style="height:145px;  padding:0">
            <HeaderLayout ref="header"></HeaderLayout>
            <el-row style="height:35px; line-height:35px; border-bottom:solid 0.5px #F6F6F6;border-top:solid 0.5px #F6F6F6;">
                <div>
                    <div style="width:201px;display: inline-block;"></div>
                    <div style="display: inline-block;">
                        <el-breadcrumb separator-class="el-icon-arrow-right" style="font-size:13px; margin-top:10px; margin-left:10px; padding-left:5px; border-left:2px solid #409EFF ">
                            <el-breadcrumb-item v-for="item in matchedRoutes" :key="item.path" :to="{ path: item.path }">{{item.meta.title}}</el-breadcrumb-item>
                        </el-breadcrumb>
                    </div>
                </div>
            </el-row>
        </el-header>

        <el-container :style="{height: (fullHeight - 145) + 'px'}">
            <el-aside :style="{height: (fullHeight - 145) + 'px',width: '201px', 'border-right':'1px solid #F6F6F6'}">
                <el-menu style="border-right:none;" :default-openeds="opendMenus" :default-active="currentRoutePath" :router="true" class="el-menu-vertical" :collapse="isCollapse">
                    <template v-for="(menu) in menuItems">
                        <el-submenu v-if="menu.category" :index="menu.category" :key="menu.category">
                            <template slot="title">
                                <i v-if="menu.icon" :class="menu.icon"></i>
                                <span slot="title">{{menu.category}}</span>
                            </template>
                            <el-menu-item v-for="item in menu.children" :key="item.path" :index="item.path">{{item.meta.title}}</el-menu-item>
                        </el-submenu>
                        <el-menu-item v-if="!menu.category" :key="menu.path" :index="menu.path">{{menu.meta.title}}</el-menu-item>
                    </template>
                </el-menu>
            </el-aside>

            <el-main :style="{height: (fullHeight - 145) + 'px'}">
                <router-view />
            </el-main>
        </el-container>
    </el-container>
</template>


<script>
import { State, Getter } from "vuex-class";
import bus from "../common/Bus";
import HeaderLayout from "../views/HeaderLayout.vue";
import router, { Categories } from "../router";
import { RouteConfig, RouteRecord } from "vue-router";
import _ from "lodash";

export default {
    components: {
        HeaderLayout
    },
    data() {
        return {
            isCollapse: false,
            asideWidth: "200px",
            routes: [],
            fullHeight: document.documentElement.clientHeight,
            currentRoutePath: "",
            menuItems: [],
            matchedRoutes: [],
            opendMenus: []
        };
    },
    beforeMount() {
        window.addEventListener("resize", this.handleResize);
        this.routes = this.$router.options.routes;
        this.$router.afterEach(this.findRoute);
        this.matchedRoutes = this.$router.currentRoute.matched;
        this.findRoute();
        this.handleResize();
    },

    methods: {
        findRoute() {
            if (this.$router.currentRoute.matched.length) {
                this.matchedRoutes = this.$router.currentRoute.matched;
                this.currentRoutePath = this.$router.currentRoute.path;

                const rootRoute = this.$router.currentRoute.matched[0];
                const currentTopRoute = this.routes.filter(
                    x => x.path === rootRoute.path
                );

                if (currentTopRoute.length > 0 && currentTopRoute[0].children) {
                    console.log(currentTopRoute[0]);
                    this.generateMenuItems(currentTopRoute[0]);
                }
            }
        },

        generateMenuItems(rootRoute) {
            const routes = rootRoute.children.map(x => {
                const r = _.clone(x);
                if (r.path) {
                    r.path =
                        r.path.indexOf("/") === 0
                            ? r.path
                            : rootRoute.path + "/" + r.path;
                } else {
                    r.path = rootRoute.path;
                }

                return r;
            });
            const items = _.partition(
                routes.filter(x => !x.meta.hidden),
                x => x.meta.category
            );
            const groupedByCategory = _.groupBy(items[0], x => x.meta.category);
            const itemsWithCategory = Object.keys(groupedByCategory).map(k => ({
                category: k,
                children: groupedByCategory[k],
                icon: Categories.filter(x => x.name === k)[0].icon
            }));
            this.opendMenus = Object.keys(groupedByCategory);
            this.menuItems = [...items[1], ...itemsWithCategory];
        },
        handleResize() {
            this.fullHeight = document.documentElement.clientHeight;
            // bus.$emit("fullHeightChanged", this.fullHeight);
        },
        toggleClick() {
            // this.isCollapse = !this.isCollapse;
            // if (this.isCollapse === true) {
            //     this.asideWidth = "55px";
            // } else {
            //     this.asideWidth = "200px";
            // }
        }
    },


};
</script>
<style scoped>
.hamburger {
    display: inline-block;
    cursor: pointer;
    width: 20px;
    height: 20px;
    transform: rotate(90deg);
    transition: 0.38s;
    transform-origin: 50% 50%;
}

.hamburger.is-active {
    transform: rotate(0deg);
}
.el-menu-vertical:not(.el-menu--collapse) {
    width: 200px;
}
</style>
