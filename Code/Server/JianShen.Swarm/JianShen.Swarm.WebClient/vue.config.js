const path = require("path");

function resolve(dir) {
    return path.join(__dirname, dir);
}

module.exports = {
    chainWebpack: config => {
        config.module.rules.delete("svg"); //�ص�:ɾ��Ĭ�������д���svg,
        //const svgRule = config.module.rule('svg')
        //svgRule.uses.clear()
        config.module
            .rule('svg-sprite-loader')
            .test(/\.svg$/)
            .include
            .add(resolve('src/icons')) //����svgĿ¼
            .end()
            .use('svg-sprite-loader')
            .loader('svg-sprite-loader')
            .options({
                symbolId: 'icon-[name]'
            });
    },
    configureWebpack: {
        name: "���ָ��",
        resolve: {
            alias: {
                '@': resolve('src')
            }
        }
    }
}