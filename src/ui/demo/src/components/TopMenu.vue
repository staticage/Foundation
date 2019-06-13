<template>
    <ul class="top-menu">
        <li :class="{item:true,'is-active': current.indexOf(route.path) > -1}" v-for="route in routes" :key="route.path" @click="onClickMenu(route)" v-if="!route.meta.hidden">
            <img class="icon" :src="('/static/images/' + (route.meta.icon || route.name) + '.png')">
            <span class="title">{{route.meta.title}}</span>
            <div class="divider"></div>
        </li>
    </ul>
</template>

<script>
import { Component, Prop, Vue } from "vue-property-decorator";
import { RouteConfig } from "vue-router";
import { Routes } from "../common/Extensions";

export default {
    data() {
        return {
            routes: [],
            current: ""
        };
    },
    mounted() {
        this.routes = this.$router.options.routes.filter(x => x.meta);
        this.$router.afterEach((to, from) => {
            const route = Routes.getCurrentRootRoute(this.$router);
            if (route) {
                this.current = route.path;
            }
        });
        this.current = Routes.getCurrentRootRoute(this.$router).path;
    },
    methods: {
        onClickMenu(route) {
            this.current = route.path;
            if (route.children && route.children.length) {
                if (route.children[0].path && route.children[0].path.startsWith("/")) {
                    this.current = route.children[0].path;
                } else {
                    this.current += `/${route.children[0].path}`;
                }
            }
            console.log(this.current);
            this.$router.push(this.current);
        }
    }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import "../element-variables";
.top-menu {
    width: 100%;
    border-radius: 2px;
    list-style: none;
    position: relative;
    margin: 0;
    padding-left: 0;
    // background-color: #eff2f7;
}
.top-menu .item {
    width: 100px;
    float: left;
    height: 75px;
    line-height: 75px;
    margin: 0;
    margin-right: 2px;
    font-family: Microsoft Yahei, PingFang SC, Hiragino Sans GB, tahoma, arial;
    cursor: pointer;
    position: relative;
    box-sizing: border-box;
    border-bottom: 3px solid transparent;
    text-align: center;
}
.top-menu .item:hover {
    border-bottom: 3px solid $--color-primary;
}

.top-menu .item > .icon {
    width: 32px;
    display: block;
    margin: 10px auto;
}

.top-menu .item > .title {
    display: block;
    line-height: 1;
    font-size: 0.8rem;
    font-weight: 400;
}
.top-menu .item > .divider {
    position: absolute;
    top: 18px;
    height: 40px;
    border-right: 2px solid #f3f3f3;
    width: 100%;
    left: 0;
}

.top-menu > .is-active {
    color: $--color-primary;
    border-bottom: 3px solid $--color-primary;
}

.title {
    font-size: 0.9rem;
    margin: 0;
    padding: 0px;
}
a {
    color: $--color-primary;
}
</style>
