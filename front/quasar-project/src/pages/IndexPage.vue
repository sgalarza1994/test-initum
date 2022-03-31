<template>
  <q-page class="q-pa-md">
    <div class="row justify-center">
      <div class="col-3">
        <q-input placeholder="Ingrese el Id" v-model="id" />
      </div>
      <div class="col-3">
        <q-input placeholder="Ingrese los nombres" class="q-ml-md" v-model="nombre" />
      </div>
      <div class="col-2">
        <q-btn label="Guardar" color="primary" @click="addTurno" />
      </div>
    </div>
    <div class="row justify-center q-mt-md">
      <div v-for="(item, index) in items.items " :key="index">
        <q-card class="q-ml-md">
            <q-toolbar class="bg-primary text-white"  >
              <q-toolbar-title> {{ item.queueName }} </q-toolbar-title>
              Pendiente : {{item.items.length}}
            </q-toolbar>
            <q-card-section>
              <q-list v-for="(turno,index) in item.items" :key="index">
              <q-item clickable  v-ripple @click="atenderTurno(turno)">
                <q-item-section>
                  <q-item-label>{{turno.registrationDate}}</q-item-label>
                  <q-item-label caption lines="2">{{turno.personName}}</q-item-label>
                </q-item-section>

                <q-item-section side top>
                  <q-item-label caption>{{turno.hourAttention}}</q-item-label>
                </q-item-section>
                </q-item>
                  <q-separator spaced inset />
              </q-list>
            </q-card-section>

        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script>
import { defineComponent, ref, onMounted, reactive } from 'vue'
import { api } from 'boot/axios'
import {Loading,Notify} from 'quasar';
export default defineComponent({
  name: 'IndexPage',
  setup() {
    var id = ref("");
    var nombre = ref("");
    const items = reactive({ 'items': [] });
    const getQueues = async () => {
      try {
        var response = await api.get('Queue/GetQueueDetail')
        items.items = response.data.result;
      }
      catch (error) {

      }

    }

    const atenderTurno = async (item) =>{
      try
      {
          var {data} = await api.post('Queue/TurnoAttention',item)
          if(!data.success)
          {
            console.log(rsp);
            alert(rsp.message);
          }
          else
          {
            await getQueues();
          }
      }
      catch(error)
      {
        console.log(error);
      }
    }

    const addTurno = async() =>{
      try
      {
        if(id == "" || nombre.value == "")
        {
          alert('Tiene campos obligatorios vacios');
          return;
        }

      var {data} = await api.post('Queue/AddTurno',{'id':id.value, 'nombre' : nombre.value});
       if(!data.success)
       {
         alert(data.message)
       }
       else
       await getQueues();
      }
      catch(error)
      {

      }

    }

    onMounted(() => {
      getQueues();
    })

    return {
      id,
      nombre,
      items,
      atenderTurno,
      addTurno
    }

  }
})
</script>
