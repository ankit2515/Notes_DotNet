const savebtn = document.querySelector('#btnSave');
const titleval=document.querySelector('#title');
const descriptionVal=document.querySelector('#description');
const noteCont=document.querySelector('#note_sidecontainer');
const deleteBtn=document.querySelector('#btnDelete');

function ClearForm(){
    titleval.value='';
    descriptionVal.value='';
    deleteBtn.classList.add('hidden');
}

function displayNoteForm(note){
   titleval.value=note.title;
   descriptionVal.value=note.description;
   deleteBtn.classList.remove('hidden');
   deleteBtn.setAttribute('data-id',note.id);
   savebtn.setAttribute('data-id',note.id);
}

function getNoteById(id){
    fetch(`https://localhost:7241/api/notes/${id}`).then(data=>data.json()).then(response=>displayNoteForm(response));
    //the response generated will be in the form of notes that will helps in showing or displaying the notes using 'displayNoteForm' function
}

function populateForm(id){
    getNoteById(id);

    //will helps in populating the form in the form of notes that exists in database with value
}

function updateNote(id,title,description){
    const body={
        Title: title,
        Description: description
     };
 
     fetch(`https://localhost:7241/api/notes/${id}`,{
         method: 'PUT',
         body: JSON.stringify(body),
         headers:{
             "content-type": "application/json"
         }
     }).then(data=>data.json()).then(response=>{
         ClearForm();
         getAllNotes();
     });
}

function addNote(title, description){
    const body={
       Title: title,
       Description: description
    };

    fetch(`https://localhost:7241/api/notes`,{
        method: 'POST',
        body: JSON.stringify(body),
        headers:{
            "content-type": "application/json"
        }
    }).then(data=>data.json()).then(response=>{
        ClearForm();
        getAllNotes();
    });
}
function displayNotes(notes){
    let allNotes='';
    notes.forEach(note => {
      const noteEle=  `
                     <div class="note" data-id="${note.id}"> 
                     <h3>${note.title}</h3>
                      <p>${note.description}</p>
                    </div>
                    `
    allNotes+=noteEle;
    });
    noteCont.innerHTML=allNotes;

    document.querySelectorAll('.note').forEach(note=> {
      note.addEventListener('click',function(){
        populateForm(note.dataset.id);
      });
    });
}
function getAllNotes(){
    fetch(`https://localhost:7241/api/notes`).then(data=>data.json()).then(response => displayNotes(response));
}

getAllNotes();

savebtn.addEventListener('click',function(){
  const id=savebtn.dataset.id;
  if(id){
    updateNote(id,titleval.value,descriptionVal.value);
  }
  else{
    addNote(titleval.value, descriptionVal.value);
  }
});

function deleteNode(id){
    fetch(`https://localhost:7241/api/notes/${id}`,{
        method: 'DELETE',
        headers:{
            "content-type": "application/json"
        }
    }).then(()=>{
        ClearForm();
        getAllNotes();
    });
}

deleteBtn.addEventListener('click',function(){
  const id=deleteBtn.dataset.id;
  deleteNode(id);
});