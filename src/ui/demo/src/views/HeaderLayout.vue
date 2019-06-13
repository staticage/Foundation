<template>
    <div @click="allClick">
        <el-container class="headerLayout">
            <el-header style="height:135px;border-bottom: 1px solid #F6F6F6">
                <el-row class="headTips">
                    <el-row style="width: 15%;height: 60px;float:right;margin-right: 15px;">
                        <el-row :gutter="5">
                            <el-col :span="3" class="btn-fullscreen fullPage" style="cursor: pointer;">
                                <div @click="handleFullScreen('screen')">
                                    <el-tooltip effect="dark" :content="fullscreen ? `取消全屏`:`全屏`" placement="bottom">
                                        <i class="el-icon-rank activeClass" :class="isClick === 'screen'?'activeClick':''" v-if="fullscreen"></i>
                                        <i class="fa fa-tv activeClass font_size" :class="isClick === 'screen'?'activeClick':''" v-if="!fullscreen"></i>
                                    </el-tooltip>
                                </div>
                            </el-col>
                            <el-col :span="4" class="menuTitleCenter">
                                <div @click="clickEvent('center')">
                                    <el-popover placement="bottom" class="tips_bell" width="400" trigger="click">
                                        <p v-if="bellMessage.length===0" style="text-align: center;">暂无消息...</p>
                                        <el-tabs v-model="activeName" type="card" @tab-click="handleClick" v-if="bellMessage.length!==0">
                                            <el-tab-pane label="待办" name="first">
                                                <div class="bellTxt" v-for="item in bellMessage" :key="item.id" @click="onBellClick(item)">
                                                    <el-button class="titleName">
                                                        <span>{{item.title}}</span>
                                                    </el-button>
                                                    <span class="bellTime">时间：{{item.timeText}}</span>
                                                </div>
                                            </el-tab-pane>
                                        </el-tabs>
                                        <el-button slot="reference">
                                            <el-badge :value="bellMessage.length===0?'':bellMessage.length" :max="99" class="item">
                                                <el-button>
                                                    <i class="el-icon-bell activeClass" :class="isClick === 'center'?'activeClick':''"></i>
                                                </el-button>
                                            </el-badge>
                                        </el-button>
                                    </el-popover>
                                </div>
                            </el-col>
                            <el-col :span="17" class="menuTitle">
                                <div @click="clickEvent('right')">
                                    <el-dropdown class="dropdownBox" trigger="click" @command="handleCommand">
                                        <span class="el-dropdown-link">
                                            <div class="avatarHeader">
                                                <img src="./../assets/images/march.jpg" alt>
                                            </div>
                                            <span :class="isClick === 'right'?'activeClick':''" class="activeClass">
                                                {{user.name}}
                                                <i class="el-icon-arrow-down el-icon--right"></i>
                                            </span>
                                        </span>
                                        <el-dropdown-menu slot="dropdown">
                                            <el-dropdown-item command="personalInfo">个人资料</el-dropdown-item>
                                            <el-dropdown-item command>公司资料</el-dropdown-item>
                                            <el-dropdown-item command="logout">退出登录</el-dropdown-item>
                                        </el-dropdown-menu>
                                    </el-dropdown>
                                </div>
                            </el-col>
                        </el-row>
                    </el-row>
                </el-row>
                <el-row>
                    <el-col :span="4" style="position: relative">
                        <img style="height:45px; margin-top:13px;" src="../assets/images/logo.png">
                        <div style="position: absolute;top: 18px;height: 40px;border-right: 2px solid #f3f3f3;width: 98%;left: 0;"></div>
                    </el-col>
                    <el-col :span="20">
                        <TopMenu></TopMenu>
                    </el-col>
                </el-row>
            </el-header>
            <el-dialog :visible.sync="dialogFormVisible" @open="userFormOper">
                <el-tabs v-model="activeName" @tab-click="chengeInfo">
                    <el-tab-pane label="个人资料" name="first">
                        <el-col :span="24">
                            <el-col :span="12">
                                <el-form ref="userForm" :rules="rules.user" :model="command.user" label-width="85px">
                                    <el-form-item label="姓名" prop="name">
                                        <el-input v-model="command.user.name" placeholder="请输入姓名"></el-input>
                                    </el-form-item>
                                    <el-form-item label="性别" prop="gender">
                                        <el-radio-group v-model="command.user.gender">
                                            <el-radio :label="1">男</el-radio>
                                            <el-radio :label="2">女</el-radio>
                                        </el-radio-group>
                                    </el-form-item>
                                    <el-form-item label="联系电话" prop="phone">
                                        <el-input v-model="command.user.phone" placeholder="请输入联系电话"></el-input>
                                    </el-form-item>
                                    <el-form-item label="电子邮箱" prop="email">
                                        <el-input v-model="command.user.email" placeholder="请输入电子邮箱"></el-input>
                                    </el-form-item>
                                    <el-form-item>
                                        <el-button type="primary" @click="onSubmit">修改基本信息</el-button>
                                    </el-form-item>
                                </el-form>
                            </el-col>
                            <el-col :span="12">
                                <el-upload class="avatar-uploader" action="https://jsonplaceholder.typicode.com/posts/" :show-file-list="false" :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload">
                                    <img v-if="imageUrl" :src="imageUrl" class="avatar">
                                    <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                                </el-upload>
                                <p class="unloadAvatar">上传头像</p>
                                <div style="text-align: left;font-size: 12px">
                                    <p>请参考如下标准上传头像</p>
                                    <p>1.图片尺寸建议：宽180px*高180px</p>
                                    <p>2.图片格式建议：推荐使用JPG/PNG格式</p>
                                </div>
                            </el-col>
                        </el-col>
                    </el-tab-pane>
                    <el-tab-pane label="修改密码" name="second">
                        <el-form :model="command.password" status-icon :rules="rules.password" ref="passwordForm" label-width="100px" class="demo-ruleForm">
                            <el-form-item label="旧密码" prop="oldPassword">
                                <el-input type="password" v-model="command.password.oldPassword" autocomplete="off" placeholder="请旧密码"></el-input>
                            </el-form-item>
                            <el-form-item label="密码" prop="newPassword">
                                <el-input type="password" v-model="command.password.newPassword" autocomplete="off" placeholder="请输入密码"></el-input>
                            </el-form-item>
                            <el-form-item label="确认密码" prop="confirmPassword">
                                <el-input type="password" v-model="command.password.confirmPassword" autocomplete="off" placeholder="请输入邮箱"></el-input>
                            </el-form-item>
                            <el-form-item>
                                <el-button type="primary" @click="changePassword">确定</el-button>
                            </el-form-item>
                        </el-form>
                    </el-tab-pane>
                </el-tabs>
                <!--<div slot="footer" class="dialog-footer">-->
                <!--<el-button @click="dialogFormVisible = false">取 消</el-button>-->
                <!--<el-button type="primary" @click="dialogFormVisible = false">确 定</el-button>-->
                <!--</div>-->
            </el-dialog>
        </el-container>
    </div>
</template>


<script>
import TopMenu from "../components/TopMenu.vue";
import moment from "moment";
import validators from "../common/validators";
import { RouteConfig, RouteRecord } from "vue-router";
import api from "../config/api";

export default {
    components: {
        TopMenu
    },
    props: {},
    data() {
        var validatePass = (rule, value, callback) => {
            if (value === '') {
                callback(new Error('请输入密码'));
            } else {
                if (this.command.password.confirmPassword !== '') {
                    this.$refs.passwordForm.validateField('confirmPassword');
                }
                callback();
            }
        };
        var validatePass2 = (rule, value, callback) => {
            if (value === '') {
                callback(new Error('请再次输入密码'));
            } else if (value !== this.command.password.newPassword) {
                callback(new Error('两次输入密码不一致!'));
            } else {
                callback();
            }
        };
        return {
            imageUrl: '',
            command: {
                user: {},
                password: {}
            },
            rules: {
                user:
                {
                    name: [{ required: true, message: "请输入姓名", trigger: "blur" }],
                    gender: [{ required: true, message: "请选择性别", trigger: "change" }],
                    phone: [
                        { required: true, message: "请输入联系电话", trigger: "blur" },
                        { validator: validators.phone, trigger: "blur" }
                    ],
                    email: [
                        {
                            type: "email",
                            message: "电子邮箱格式错误",
                            trigger: "blur"
                        }
                    ]
                },
                password: {
                    oldPassword: [
                        { required: true, message: "请输入旧密码", trigger: "blur" }
                    ],
                    newPassword: [
                        { required: true, message: "请输入密码", trigger: "blur" },
                        { validator: validatePass, trigger: 'blur' }
                    ],
                    confirmPassword: [
                        { required: true, message: "请输入确认密码", trigger: "blur" },
                        { validator: validatePass2, trigger: 'blur' }
                    ]
                }
            },
            activeName: "first",
            visible: false,
            user: {},
            bellMessage: [],
            fullscreen: false,
            activeInfo: 'second',
            dialogFormVisible: false,
            isClick: ''
        };
    },

    mounted() {
        this.user = this.$store.state.user;
        this.getUserBackLog();
    },
    methods: {
        handleAvatarSuccess(res, file) {
            this.imageUrl = URL.createObjectURL(file.raw);
        },
        beforeAvatarUpload(file) {
            console.log(file, 'lll')
            const isJPG = file.type === 'image/jpeg' || file.type === 'image/png';
            const isLt2M = file.size / 1024 / 1024 < 2;

            if (!isJPG) {
                this.$message.error('上传头像图片只能是 JPG/PNG 格式!');
            }
            if (!isLt2M) {
                this.$message.error('上传头像图片大小不能超过 2MB!');
            }
            return isJPG && isLt2M;
        },
        async getUserBackLog() {
            var result = [];
            // (await this.$axios.get("user/backlog")).data.map(x => {
            //     return {...x, timeText: moment(x.time).format("YYYY-MM-DD")};
            // });
            this.bellMessage = result;
        },
        async userFormOper() {
            // if (JSON.stringify(this.command.user) === '{}') {
            //     this.command.user = (await this.$axios.get(api.currentUser)).data;
            //     console.log(this.command.user);
            // }
        },
        onBellClick(item) {
            this.$router.push(item.link + "&bid=" + item.id);
            // this.$router.push({
            //     name: "contract-approve",
            //     query: {bid: item.id}
            // });
            // localStorage.setItem("currentBacklogId", item.id);
            //,{params:item}
            // this.$router.push({
            //     path: item.link,
            //     query: {bid: item.id}
            //     // cid rid
            //     // ContractId RunningWorkflowId
            // });
        },
        handleCommand(command) {
            switch (command) {
                case "logout":
                    this.logout();
                    break;
                case "personalInfo":
                    this.personalInfo();
                    break;
            }
        },
        changePassword() {
            this.$refs.passwordForm.validate(valid => {
                if (valid) {
                    this.loading = true;
                    this.$axios.post(api.user + "/change-password", this.command.password).then(
                        response => {
                            this.loading = false;
                            this.$message({
                                message: "修改成功",
                                type: "success"
                            });
                            this.dialogFormVisible = false;
                            this.command.password = {};
                        },
                        err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "修改失败:" + err.message
                            });
                        }
                    );
                } else {
                    return false;
                }
            });
        },
        handleChange(value) {
            console.log(value);
        },
        allClick(e) {
            if (!e.target.className.includes('activeClass')) {
                this.isClick = ''
            }
        },
        clickEvent(type) {
            this.isClick = type
        },
        chengeInfo(tab, event) {
            console.log(tab, event);
        },
        logout() {
            localStorage.clear();
            this.$router.push({ name: "login" });
        },
        onSubmit() {
            this.$refs.userForm.validate(valid => {
                if (valid) {
                    this.loading = true;
                    this.$axios.put(api.currentUser, this.command.user).then(
                        response => {
                            this.loading = false;
                            this.$message({
                                message: "保存成功",
                                type: "success"
                            });
                            this.dialogFormVisible = false;
                        },
                        err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "保存失败:" + err.message
                            });
                        }
                    );
                } else {
                    return false;
                }
            });
        },
        personalInfo() {
            this.dialogFormVisible = true;

        },
        handleClick(tab, event) {
        },
        handleFullScreen(type) {
            this.isClick = type
            let element = document.documentElement;
            if (this.fullscreen) {
                if (document.exitFullscreen) {
                    document.exitFullscreen();
                } else if (document.webkitCancelFullScreen) {
                    document.webkitCancelFullScreen();
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.msExitFullscreen) {
                    document.msExitFullscreen();
                }
            } else {
                if (element.requestFullscreen) {
                    element.requestFullscreen();
                } else if (element.webkitRequestFullScreen) {
                    element.webkitRequestFullScreen();
                } else if (element.mozRequestFullScreen) {
                    element.mozRequestFullScreen();
                } else if (element.msRequestFullscreen) {
                    element.msRequestFullscreen();
                }
            }
            this.fullscreen = !this.fullscreen;
        }
    }
};
</script>

<style lang="scss">
.bellTxt .el-button {
    border: none;
    background: none;
}

/*.el-tooltip__popper.is-dark {*/
/*width: 200px;*/
/*background: #74dacf;*/
/*}*/
.tips_bell .el-button--small {
    padding: 5px 10px;
    font-size: 20px;
    border-radius: 3px;
    background: transparent;
    border: none;
    color: #fff;
}

.tips_bell .el-badge__content.is-fixed {
    position: absolute;
    top: 10px;
    right: 20px;
}

.tips_bell .el-badge__content {
    border: 1px solid #f56c6c;
}

.dropdownBox {
    height: 60px;
    line-height: 60px;
    color: #fff;
    cursor: pointer;
}

.el-dropdown-menu {
    margin-top: -1px !important;
}

.headerLayout {
    /*.el-form-item {*/
    /*width: 70%;*/
    /*}*/
    .el-tabs__content {
        margin-top: 20px;
        border: none;
    }

    /*.el-dialog{*/
    /*width: 30%;*/
    /*}*/
    /*.el-dialog__header {*/
    /*background: #409EFF;*/
    /*}*/
    .el-dialog__body {
        padding: 0 20px;
    }

    .el-form-item--small.el-form-item {
        margin-left: -30px;
    }

    @media screen and (max-width: 1366px) {
        .el-input--small,
        .el-input-number--small {
            width: 260px;
        }
    }
    @media screen and (min-width: 1920px) {
        .el-input--small,
        .el-input-number--small {
            width: 300px;
        }
    }

    .avatar-uploader .el-upload {
        border: 1px dashed #d9d9d9;
        border-radius: 6px;
        cursor: pointer;
        position: relative;
        overflow: hidden;
    }

    .avatar-uploader .el-upload:hover {
        border-color: #409eff;
    }

    .avatar-uploader-icon {
        font-size: 28px;
        color: #8c939d;
        width: 180px;
        height: 180px;
        line-height: 180px;
        text-align: center;
    }

    .avatar {
        width: 178px;
        height: 178px;
        display: block;
    }
}
</style>
<style lang="scss" scoped>
.headTips {
    height: 60px;
    background-color: #42b983;
    margin: 0 -20px;
    padding: 0;
    position: relative;

    .fullPage {
        color: #fff;
        font-size: 20px;
        padding-top: 15px;
        float: left;
    }

    .activeClick {
        color: #f56c6c;
        border: none;
    }

    .menuTitleCenter {
        color: #fff;
        right: 0;
        padding-top: 10px;
        text-align: center;
        float: left;
    }

    .menuTitle {
        height: 100%;
        color: #fff;
        font-size: 20px;
        padding-bottom: 20px;
        float: right;
    }

    .tips_bell {
        color: #fff;
        font-size: 20px;
    }

    .avatarHeader {
        float: left;
        padding: 10px 10px 0 20px;

        img {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            border: 1px solid #42b983;
        }
    }
}

.dropdown-bell-line {
    width: 100%;
    float: left;
}

.bellTxt {
    width: 100%;
    height: 30px;
}

.bellTxt span {
    display: inline-block;
}

.el-dropdown-menu_ {
    width: 25%;

    .el-dropdown-menu__item {
        padding: 0;
    }
}

.titleName,
.titleName {
    width: 60%;
    float: left;
    text-align: left;
    font-size: 12px;
}

.bellTime {
    float: right;
    width: 40%;
    text-align: right;
    font-size: 12px;
    padding: 5px 0px;
}

.font_size {
    font-size: 14px;
}

.unloadAvatar {
    height: 30px;
    font-size: 16px;
    width: 180px;
    background: #409eff;
    border: 1px solid #409eff;
    border-radius: 3px;
    text-align: center;
    line-height: 30px;
    color: #fff;
    cursor: pointer;
}
</style>
