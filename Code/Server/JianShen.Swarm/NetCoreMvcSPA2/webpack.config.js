const path = require('path');
const bundleOutputDir = './wwwroot/dist';
const VueLoaderPlugin = require('vue-loader/lib/plugin');

module.exports = {
    resolve: {
        extensions: ['.js', '.vue']
    },
    entry: './ClientApp/main.js',
    module: {
        rules: [
            { test: /\.vue$/, include: /ClientApp/, loader: 'vue-loader' }
        ]
    },
    output: {
        path: path.join(__dirname, bundleOutputDir),
        filename: '[name].js',
        publicPath: 'dist/'
    },
    plugins: [
        new VueLoaderPlugin()
    ],
    devServer: {
        contentBase: path.resolve(__dirname, 'dist'),// 配置开发服务运行时的文件根目录
        host: 'localhost',// 开发服务器监听的主机地址
        compress: true,   // 开发服务器是否启动gzip等压缩
        port: 5000        // 开发服务器监听的端口
    }
    
}