import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import {
  Container,
  Grid,
  Paper,
  Typography,
  Box,
  Tabs,
  Tab,
  Button,
  List,
  ListItem,
  ListItemText,
  Divider,
  Chip,
} from '@mui/material';
import Navbar from '../components/Navbar';
import api from '../services/api';

interface Gym {
  id: string;
  name: string;
  description: string;
  address: string;
  phone: string;
  email: string;
  imageUrl?: string;
}

interface Class {
  id: string;
  name: string;
  description: string;
  instructor: string;
  schedule: string;
}

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`gym-tabpanel-${index}`}
      aria-labelledby={`gym-tab-${index}`}
      {...other}
    >
      {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
    </div>
  );
}

const GymDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [gym, setGym] = useState<Gym | null>(null);
  const [classes, setClasses] = useState<Class[]>([]);
  const [tabValue, setTabValue] = useState(0);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchGymDetails = async () => {
      try {
        const [gymResponse, classesResponse] = await Promise.all([
          api.get(`/gym/${id}`),
          api.get(`/class?gymId=${id}`),
        ]);
        setGym(gymResponse.data);
        setClasses(classesResponse.data);
      } catch (error) {
        console.error('Error fetching gym details:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchGymDetails();
  }, [id]);

  const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
    setTabValue(newValue);
  };

  if (loading) {
    return (
      <>
        <Navbar />
        <Container>
          <Typography>Loading...</Typography>
        </Container>
      </>
    );
  }

  if (!gym) {
    return (
      <>
        <Navbar />
        <Container>
          <Typography>Gym not found</Typography>
        </Container>
      </>
    );
  }

  return (
    <>
      <Navbar />
      <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
              <Box
                sx={{
                  height: 300,
                  backgroundImage: `url(${gym.imageUrl || 'https://source.unsplash.com/random?gym'})`,
                  backgroundSize: 'cover',
                  backgroundPosition: 'center',
                  mb: 2,
                }}
              />
              <Typography variant="h4" component="h1" gutterBottom>
                {gym.name}
              </Typography>
              <Typography variant="body1" paragraph>
                {gym.description}
              </Typography>
              <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
                <Chip icon={<span>📍</span>} label={gym.address} />
                <Chip icon={<span>📞</span>} label={gym.phone} />
                <Chip icon={<span>✉️</span>} label={gym.email} />
              </Box>
            </Paper>
          </Grid>

          <Grid item xs={12}>
            <Paper sx={{ width: '100%' }}>
              <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs value={tabValue} onChange={handleTabChange}>
                  <Tab label="Classes" />
                  <Tab label="Instructors" />
                  <Tab label="Plans" />
                </Tabs>
              </Box>

              <TabPanel value={tabValue} index={0}>
                <List>
                  {classes.map((classItem) => (
                    <React.Fragment key={classItem.id}>
                      <ListItem>
                        <ListItemText
                          primary={classItem.name}
                          secondary={
                            <>
                              <Typography component="span" variant="body2">
                                {classItem.description}
                              </Typography>
                              <br />
                              <Typography component="span" variant="body2">
                                Instructor: {classItem.instructor}
                              </Typography>
                              <br />
                              <Typography component="span" variant="body2">
                                Schedule: {classItem.schedule}
                              </Typography>
                            </>
                          }
                        />
                        <Button variant="contained" color="primary">
                          Book Now
                        </Button>
                      </ListItem>
                      <Divider />
                    </React.Fragment>
                  ))}
                </List>
              </TabPanel>

              <TabPanel value={tabValue} index={1}>
                <Typography>Instructors information will be displayed here</Typography>
              </TabPanel>

              <TabPanel value={tabValue} index={2}>
                <Typography>Available plans will be displayed here</Typography>
              </TabPanel>
            </Paper>
          </Grid>
        </Grid>
      </Container>
    </>
  );
};

export default GymDetail; 