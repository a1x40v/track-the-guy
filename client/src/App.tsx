import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import { MainNav } from './components/common/layout/MainNav';
import { HomePage } from './components/pages/HomePage';
import { CharactersPage } from './components/pages/CharactersPage';
import RegisterForm from './components/features/auth/RegisterForm';
import LoginForm from './components/features/auth/LoginForm';

function App() {
  return (
    <Router>
      <div className="container mx-auto">
        <MainNav />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/register" element={<RegisterForm />} />
          <Route path="/login" element={<LoginForm />} />
          <Route path="/characters" element={<CharactersPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
