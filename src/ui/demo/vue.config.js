const path = require("path");

const resolve = dir => {
    return path.join(__dirname, dir);
};

module.exports = {
    chainWebpack: config => {
        config.module.rules.delete("eslint");
    },
    configureWebpack: {
        devtool: "#source-map"
    },
    chainWebpack: config => {
        config.module.rules.delete("eslint");
        config.resolve.alias
            .set("@", resolve("src")) // key,value自行定义，比如.set('@@', resolve('src/components'))
            .set("$c", resolve("src/components"))
            .set("$v", resolve("src/views"));
    }
};
