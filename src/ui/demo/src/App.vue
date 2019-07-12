<template>
    <div id="app">
        <!-- <el-container style="height: 610px;">
            <el-header style="height:145px">
                <el-row style="height:35px; margin-top:5px; border-bottom:solid 1px #F6F6F6; border-right:solid 1px #F6F6F6;">123123123</el-row>
            </el-header>
            <el-main>
                <el-container style="border-bottom:solid 1px #F6F6F6;height: 500px;">
                    <el-main>
                        <CustomForm />
                    </el-main>
                </el-container>
            </el-main>
        </el-container>-->
        <router-view />
    </div>
</template>

<script>
import CustomForm from "./views/system/custom-form/index";
export default {
    components: { CustomForm }


};
</script>

<style lang="scss">
html,
body {
    overflow: hidden;
    margin: 0;
    padding: 0;
}
#app {
    font-family: "Segoe UI", "Lucida Grande", Helvetica, Arial,
        "Microsoft YaHei", FreeSans, Arimo, "Droid Sans", "wenquanyi micro hei",
        "Hiragino Sans GB", "Hiragino Sans GB W3", Arial, sans-serif;
    /*font-size: 12px;*/
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
}
</style>
<style lang="scss">
.filter-content {
    box-sizing: border-box;
    color: rgb(50, 50, 50);
    display: inline-block;
}
.filter-item {
    box-sizing: border-box;
    display: inline-block;
    cursor: pointer;
    vertical-align: middle;
    padding: 0 10px;
    color: #666;
    line-height: 24px;
    text-decoration: none;
    margin-right: 10px;
}

.filter-buttons button {
    margin-left: 10px;
}
.filter-item.active {
    border: 1px solid #409eff;
    color: #409eff;
    background: #ade3ed;
    border-radius: 3px;
    font-size: 12px;
}
</style>
