const express = require('express');
const bodyParser = require('body-parser');
const path = require('path');
require('./models/db');
const app = express();

const PORT = process.env.PORT || 5000;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(require('cors')());
app.use(require('helmet')());
app.use('/api/students', require('./routes/students'));
app.use('/api/workspaces', require('./routes/workspaces'));
app.use('/api/boards', require('./routes/board'));
app.use('/api/cards', require('./routes/card')); 
app.use('/api/note', require('./routes/note')); 
app.use('/api/note', require('./routes/BoardActivity'));  





// Production
if (process.env.NODE_ENV === 'production') {
  // Set static folder
  app.use(express.static('client/build'));

  app.get('*', (req, res) => {
    res.sendfile(path.resolve(__dirname, 'client', 'build', 'index.html'));
  });
}

app.listen(PORT, () => console.log(`App running on port ${PORT}`)  );