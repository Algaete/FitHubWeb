import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { AuthProvider, useAuth } from './context/AuthContext';

// Importar páginas
const Login = React.lazy(() => import('./pages/Login'));
const Register = React.lazy(() => import('./pages/Register'));
const Dashboard = React.lazy(() => import('./pages/Dashboard'));
const GymList = React.lazy(() => import('./pages/GymList'));
const GymDetail = React.lazy(() => import('./pages/GymDetail'));

// Tema personalizado
const theme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

// Componente para rutas protegidas
const PrivateRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { isAuthenticated, loading } = useAuth();

  if (loading) {
    return <div>Loading...</div>;
  }

  return isAuthenticated ? <>{children}</> : <Navigate to="/login" />;
};

// Componente para rutas públicas (login/register)
const PublicRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { isAuthenticated, loading } = useAuth();

  if (loading) {
    return <div>Loading...</div>;
  }

  return !isAuthenticated ? <>{children}</> : <Navigate to="/" />;
};

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AuthProvider>
        <Router>
          <React.Suspense fallback={<div>Loading...</div>}>
            <Routes>
              {/* Rutas públicas */}
              <Route
                path="/login"
                element={
                  <PublicRoute>
                    <Login />
                  </PublicRoute>
                }
              />
              <Route
                path="/register"
                element={
                  <PublicRoute>
                    <Register />
                  </PublicRoute>
                }
              />

              {/* Rutas protegidas */}
              <Route
                path="/"
                element={
                  <PrivateRoute>
                    <Dashboard />
                  </PrivateRoute>
                }
              />
              <Route
                path="/gyms"
                element={
                  <PrivateRoute>
                    <GymList />
                  </PrivateRoute>
                }
              />
              <Route
                path="/gyms/:id"
                element={
                  <PrivateRoute>
                    <GymDetail />
                  </PrivateRoute>
                }
              />

              {/* Redirección por defecto */}
              <Route path="*" element={<Navigate to="/login" replace />} />
            </Routes>
          </React.Suspense>
        </Router>
      </AuthProvider>
    </ThemeProvider>
  );
}

export default App; 