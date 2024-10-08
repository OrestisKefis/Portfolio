<template>
    <Modal ref="modal" :min-width="'none'" :max-width="'25rem'" :title="'Delete'">
        <div class="m-auto mt-4 d-flex flex-column gap-4 p-3">
            <div class="fs-3 mb-3" style="max-width: 20ch;">
                Are you sure you want to delete current project
            </div>

            <div class="d-flex justify-content-evenly">
                <div>
                    <OutlineButton @click="modal.hide">
                        Cancel
                    </OutlineButton>
                </div>

                <div>
                    <Button @click="onDelete">
                        Confirm
                    </Button>
                </div>
            </div>
        </div>
    </Modal>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useProjectStore } from '@/stores/projectStore';
import { deleteProject } from '@/services/adminService';

import Modal from '../Modal.vue';
import Button from '../Button.vue';
import OutlineButton from '../OutlineButton.vue';

const emits = defineEmits(['deleted'])

const store = useProjectStore()

const modal = ref<Components.Modal>()

const onDelete = async () => {
    const response = await deleteProject(store.currentProject.id)
    response && emits('deleted')
}

const show = () => modal.value.show()

const hide = () => modal.value.hide()

defineExpose({
    show,
    hide
})
</script>