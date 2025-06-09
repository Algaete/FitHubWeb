import React from 'react';
import { Container, Typography, Box, Paper } from '@mui/material';
import { useAuth } from '../context/AuthContext';

const Dashboard: React.FC = () => {
  const { user } = useAuth();

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Typography variant="h4" component="h1" gutterBottom>
        Dashboard
      </Typography>
      <Typography variant="h6" gutterBottom>
        Bienvenido, {user?.name}
      </Typography>

      <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 3 }}>
        <Paper
          sx={{
            p: 2,
            display: 'flex',
            flexDirection: 'column',
            height: 240,
            flex: '1 1 45%',
            minWidth: 300,
          }}
        >
          <Typography variant="h6" gutterBottom>
            Próximas Clases
          </Typography>
          <Box sx={{ flexGrow: 1 }}>
            {/* Aquí irá el contenido de las próximas clases */}
          </Box>
        </Paper>

        <Paper
          sx={{
            p: 2,
            display: 'flex',
            flexDirection: 'column',
            height: 240,
            flex: '1 1 45%',
            minWidth: 300,
          }}
        >
          <Typography variant="h6" gutterBottom>
            Mi Plan Actual
          </Typography>
          <Box sx={{ flexGrow: 1 }}>
            {/* Aquí irá el contenido del plan actual */}
          </Box>
        </Paper>

        <Paper
          sx={{
            p: 2,
            display: 'flex',
            flexDirection: 'column',
            height: 240,
            flex: '1 1 100%',
          }}
        >
          <Typography variant="h6" gutterBottom>
            Actividad Reciente
          </Typography>
          <Box sx={{ flexGrow: 1 }}>
            {/* Aquí irá el contenido de la actividad reciente */}
          </Box>
        </Paper>
      </Box>
    </Container>
  );
};

export default Dashboard; 