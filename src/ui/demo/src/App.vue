<template>
    <div id="app">
        <router-view/>
    </div>
</template>

<script>
export default {

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
