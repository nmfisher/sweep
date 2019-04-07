module.exports = {
  presets: [
    '@vue/app',
    ["@babel/preset-env", { "modules": "commonjs"}],
  ],
  plugins: ["add-module-exports"]
}
