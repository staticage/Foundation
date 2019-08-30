<template>
    <div class="single-page">
        <el-card class="login-box">
            <div class="logo"></div>
            <div class="title">登录您的账号</div>
            <el-form ref="form" :rules="rules" :model="command">
                <el-row>
                    <el-col :span="24">
                        <el-input v-model="command.username" placeholder="邮箱/手机"></el-input>
                    </el-col>
                    <el-col :span="24" style="margin-top:36px;">
                        <el-input ref="password" type="password" v-model="command.password" placeholder="密码"></el-input>
                    </el-col>
                    <el-col :span="24" style="margin-top:36px;">
                        <el-button :loading="loading" type="success" plain style="width:100%;" @click="login">登录</el-button>
                    </el-col>
                </el-row>
            </el-form>
        </el-card>
    </div>
</template>


<script>
// import ElementUI from "element-ui";
import api from "../config/api";
import * as types from "../store/types";
import axios from "axios";

// import _ from "lodash";

export default {
    data() {
        return {
            command: {},
            loading: false,
            rules: {}
        };
    },
    mounted() {
        var $this = this;
        this.$refs["password"].$refs["input"].addEventListener("keydown", function (
            e
        ) {
            if (e.key === "Enter") {
                $this.login();
            }
        });
        // console.log("moto");
    },

    methods: {
        login() {
            this.loading = true;
            let $this = this;
            this.$store.set



            this.$router.push("/home");
            const loggedInUser = {
                superAdmin: true,
                name: '超级管理员',
                token: '123456'
            }

            this.$store.commit(types.LOGIN, {
                token: loggedInUser.token,
                user: {
                    // id: loginUser.id,
                    name: loggedInUser.name,
                    companyId: loggedInUser.companyId,
                    // companyName: loginUser.companyName,
                    superAdmin: loggedInUser.superAdmin,
                    // companyAdmin: loginUser.companyAdmin,
                    // userPermissions: loginUser.userPermissions
                }
            });
            console.log('login ok')
        }
    }
};
</script>
<style>
html,
body,
#app {
    width: 100%;
    height: 100%;
}
body {
    margin: 0;
    padding: 0;
}
.login-box {
    width: 320px;
    max-width: 320px;
    padding: 32px;
    background: #fff;
    margin: auto;
    text-align: center;
}
.single-page {
    box-sizing: border-box;
    display: -ms-flexbox;
    display: flex;
    align-items: center;
    -ms-flex-line-pack: center;
    align-content: center;
    max-width: 100%;
    height: 100%;
    background: url(../assets/images/march.jpg) no-repeat;
    background-size: cover;
    -webkit-background-size: cover;
    -moz-background-size: cover;
    -o-background-size: cover;
}

.title {
    font-size: 17px;
    margin: 16px 0 32px 0;
}

.logo {
    font-size: 86px;
    font-weight: 500;
    color: #fff;
    border-radius: 2px;
}
img {
    max-width: 100%;
    height: auto;
    /* vertical-align: top; */
    border: none;
}
form .md-button {
    width: 220px;
    margin: 16px auto;
    display: block;
}
</style>
