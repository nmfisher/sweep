const path = require('path');

module.exports = {
  entry: './src/index.js',
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'main.js',
    library: 'sweep',
    libraryTarget: 'umd',
        globalObject: 'this'
  },
  module: {
    rules: [
      {
          test: /\.js$/,
          use: 'babel-loader',
      },
    ]
  },
};