// main.js — JavaScript Exercises for Community Portal
// Task 1: Basic setup
console.log('Welcome to the Community Portal');
window.addEventListener('load', () => { alert('Page loaded — welcome to the Community Portal'); });

// Data model: Event class (task 5)
class EventItem {
  constructor(id, name, date, seats, category, location) {
    this.id = id; this.name = name; this.date = new Date(date); this.seats = seats; this.category = category; this.location = location;
  }
}

EventItem.prototype.checkAvailability = function(){ return this.seats > 0 && this.date >= new Date(); };

// In-memory store (task 6)
let events = [
  new EventItem(1, 'Community Concert', '2099-12-01', 50, 'music', 'Park'),
  new EventItem(2, 'Farmers Market', '2099-11-20', 10, 'market', 'Main Square'),
  new EventItem(3, 'River Clean-up', '2099-10-05', 0, 'community', 'Riverbank') // full
];

// Task 4: addEvent, registerUser, filterEventsByCategory
function addEvent(e){ events.push(e); renderEvents(); }

// closure to track registrations per category
function registrationTracker(){
  const counts = {};
  return function(category){ counts[category] = (counts[category]||0)+1; return counts[category]; };
}
const trackRegistration = registrationTracker();

function registerUser(eventId, name, email){
  try {
    const ev = events.find(x=>x.id===eventId);
    if (!ev) throw new Error('Event not found');
    if (!ev.checkAvailability()) throw new Error('Event is full or in the past');
    ev.seats--; // use -- (task 2)
    const regCount = trackRegistration(ev.category);
    console.log(`Registered ${name} (${email}) for ${ev.name}. Category registrations: ${regCount}`);
    renderEvents();
    return true;
  } catch(err){ console.error('Registration failed', err); return false; }
}

function filterEventsByCategory(category, cb){
  const list = (category==='all') ? events : events.filter(e=>e.category===category);
  if (cb && typeof cb==='function') cb(list);
  return list;
}

// Task 3: only show upcoming events with seats
function validEvents(){ return events.filter(e => e.checkAvailability()); }

// Task 7: DOM rendering
const eventsContainer = document.getElementById('events');
const categoryFilter = document.getElementById('categoryFilter');
const searchInput = document.getElementById('search');
const loadBtn = document.getElementById('loadBtn');
const spinner = document.getElementById('spinner');

function renderEvents(list = events){
  // show only upcoming with seats by default
  const out = list.filter(e => e.checkAvailability());
  eventsContainer.innerHTML = '';
  out.forEach(ev => {
    const card = document.createElement('div'); card.className = 'card';
    const title = document.createElement('h3'); title.textContent = `${ev.name}`;
    const meta = document.createElement('p'); meta.textContent = `${ev.category} • ${ev.location} • Seats: ${ev.seats}`;
    const btn = document.createElement('button'); btn.textContent = 'Register';
    btn.onclick = () => {
      const name = prompt('Your name');
      const email = prompt('Your email');
      if (name && email) registerUser(ev.id, name, email);
    };
    card.appendChild(title); card.appendChild(meta); card.appendChild(btn);
    eventsContainer.appendChild(card);
  });
  populateFormOptions();
}

function populateFormOptions(){
  const sel = document.querySelector('form[name="regForm"] select[name="eventId"]') || document.querySelector('form select[name="eventId"]');
  if (!sel) return;
  sel.innerHTML = '';
  events.forEach(e => { const opt = document.createElement('option'); opt.value=e.id; opt.textContent = `${e.name} (${e.seats} seats)`; sel.appendChild(opt); });
}

// Task 8: event handling for filter and search
categoryFilter.onchange = () => {
  const val = categoryFilter.value;
  const filtered = filterEventsByCategory(val);
  renderEvents(filtered);
};

searchInput.addEventListener('keydown', (e)=>{
  const q = searchInput.value.toLowerCase();
  const result = events.filter(ev => ev.name.toLowerCase().includes(q));
  renderEvents(result);
});

// Form handling (task 11)
const regForm = document.getElementById('regForm');
regForm.addEventListener('submit', function(ev){
  ev.preventDefault();
  const name = regForm.elements['name'].value;
  const email = regForm.elements['email'].value;
  const eventId = Number(regForm.elements['eventId'].value);
  if (!name || !email) { alert('Please fill name and email'); return; }
  const ok = registerUser(eventId, name, email);
  if (ok) {
    // Task 12: simulate POST via fetch
    simulatePost({name,email,eventId}).then(r=>alert('Registration saved (simulated)')).catch(()=>alert('Save failed'));
  }
});

// Task 9: async fetch events from mock API (simulate)
function fetchEventsMock(){
  return new Promise((res)=> setTimeout(()=> res([
    {id:4,name:'Street Music',date:'2099-09-01',seats:20,category:'music',location:'Main St'},
    {id:5,name:'Bake Workshop',date:'2099-08-15',seats:5,category:'community',location:'Hall'}
  ]), 1200));
}

async function loadEvents(){
  spinner.style.display='inline';
  try{
    const fetched = await fetchEventsMock(); // async/await example
    // merge safely using spread (task 10)
    events = [...events, ...fetched.map(f=>new EventItem(f.id,f.name,f.date,f.seats,f.category,f.location))];
    renderEvents();
  }catch(e){ console.error('Failed to load events', e); }
  spinner.style.display='none';
}

loadBtn.addEventListener('click', ()=>{ loadEvents(); });

// Task 12: simulate POST via fetch to jsonplaceholder (AJAX)
function simulatePost(data){
  // show spinner effect using setTimeout
  return new Promise((resolve,reject)=>{
    setTimeout(()=>{
      fetch('https://jsonplaceholder.typicode.com/posts', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(data) })
        .then(r=>r.json()).then(json=>resolve(json)).catch(err=>reject(err));
    }, 800);
  });
}

// Demonstrate .then()/.catch() alternative
function loadWithThen(){
  spinner.style.display='inline';
  fetchEventsMock().then(items=>{
    events = [...events, ...items.map(f=>new EventItem(f.id,f.name,f.date,f.seats,f.category,f.location))];
    renderEvents();
  }).catch(err=>console.error(err)).finally(()=>spinner.style.display='none');
}

// Task 14: jQuery demo (click & fade)
$(document).ready(()=>{
  // example: hide all cards and then fade in
  $('#loadBtn').on('click', ()=>{ $('.card').hide(); $('.card').fadeIn(600); });
  // simple jQuery register button (if present)
  $(document).on('click', '#jqueryRegister', ()=>{ alert('jQuery handler example'); });
});

// Task 13: debugging helpers
console.log('Events initial:', events);

// Task 2 examples: const/let and template literals
const sampleName = events[0].name; let seatsLeft = events[0].seats; console.log(`Event: ${sampleName} — seats ${seatsLeft}`);

// Initialize UI
renderEvents();
