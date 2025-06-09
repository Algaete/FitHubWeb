import React, { useEffect, useState } from 'react';
import { Container, Typography, Box, Paper, TextField, InputAdornment } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

interface Gym {
  id: string;
  name: string;
  description: string;
  address: string;
  phone: string;
  email: string;
  imageUrl: string;
}

const GymList: React.FC = () => {
  const [gyms, setGyms] = useState<Gym[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchGyms = async () => {
      try {
        const response = await api.get('/api/gym');
        setGyms(response.data);
      } catch (error) {
        console.error('Error fetching gyms:', error);
      }
    };

    fetchGyms();
  }, []);

  const filteredGyms = gyms.filter(gym =>
    gym.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
    gym.address.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Typography variant="h4" component="h1" gutterBottom>
        Gimnasios
      </Typography>

      <TextField
        fullWidth
        variant="outlined"
        placeholder="Buscar gimnasios..."
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        sx={{ mb: 4 }}
        InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <SearchIcon />
            </InputAdornment>
          ),
        }}
      />

      <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 3 }}>
        {filteredGyms.map((gym) => (
          <Paper
            key={gym.id}
            sx={{
              p: 2,
              display: 'flex',
              flexDirection: 'column',
              flex: '1 1 300px',
              cursor: 'pointer',
              '&:hover': {
                boxShadow: 6,
              },
            }}
            onClick={() => navigate(`/gyms/${gym.id}`)}
          >
            <Box
              component="img"
              src={gym.imageUrl || 'https://via.placeholder.com/300x200'}
              alt={gym.name}
              sx={{
                width: '100%',
                height: 200,
                objectFit: 'cover',
                mb: 2,
                borderRadius: 1,
              }}
            />
            <Typography variant="h6" gutterBottom>
              {gym.name}
            </Typography>
            <Typography variant="body2" color="text.secondary" paragraph>
              {gym.description}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              {gym.address}
            </Typography>
          </Paper>
        ))}
      </Box>
    </Container>
  );
};

export default GymList; 