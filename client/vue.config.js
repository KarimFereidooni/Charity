// tslint:disable-next-line: no-console
console.log("NODE_ENV: " + process.env.NODE_ENV);

module.exports = {
  outputDir: "../server/wwwroot/dist/",
  devServer: {
    public: "localhost",
    host: "127.0.0.1"
  },
  publicPath: process.env.NODE_ENV === "production" ? "/dist" : "/",
  // transpileDependencies: ["vuetify"],
  runtimeCompiler: true,
  productionSourceMap: false
};
