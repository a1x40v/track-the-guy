import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import { MainNav } from './components/common/layout/MainNav';
import { HomePage } from './components/pages/HomePage';
import { CharactersPage } from './components/pages/CharactersPage';

function App() {
  return (
    <Router>
      <div className="App">
        <MainNav />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/characters" element={<CharactersPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
