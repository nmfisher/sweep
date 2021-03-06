const path = require('path');

module.exports = {
  entry: './src/index.js',
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'bundle.js',
    library: 'Sweep',
  },
  mode:"none",
  module: {
    rules: [
      {
          test: /\.js$/,
          use: 'babel-loader',
      },
    ]
  },
};